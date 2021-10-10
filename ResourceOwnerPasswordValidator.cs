using IdentityModel;
using IdentityServer.Infrastructure.Cryptography;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using IdentityServerAPI.DataAccess.User;
using IdentityServerAPI.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServerAPI
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        //repository to get user from db
        private readonly IUserRepository _userRepository;
        private IPasswordHash _passwordHash;
        private readonly ILogger<ResourceOwnerPasswordValidator> _logger;
        public ResourceOwnerPasswordValidator(IUserRepository userRepository, IPasswordHash passwordHash, ILogger<ResourceOwnerPasswordValidator> logger)
        {
            _userRepository = userRepository;
            _passwordHash = passwordHash;
            _logger = logger;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {

            try
            {
                var user = await _userRepository.GetByUsernameAsync(context.UserName);
                if (user == null)
                {
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "User does not exist.");
                    return;
                }

                if (_passwordHash.VerifySignature(context.Password,
                    Convert.FromBase64String(user.Password), Convert.FromBase64String(user.PasswordSalt)))
                {
                    context.Result = new GrantValidationResult(
                             subject: user.UserId.ToString(),
                             authenticationMethod: "custom",
                             claims: GetUserClaims(user));
                    return;
                }
                else
                {
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Incorrect password");
                    return;
                }
            }
            catch (Exception ex)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid username or password");
                _logger.LogError("error in ResourceOwnerPasswordValidator/ ValidateAsync", ex);
            }
        }

        public static Claim[] GetUserClaims(UserModel user)
        {
            return new Claim[]
            {
            new Claim("user_id", user.UserId.ToString() ?? ""),
            new Claim(JwtClaimTypes.Name, (!string.IsNullOrEmpty(user.FirstName) && !string.IsNullOrEmpty(user.LastName)) ? (user.FirstName + " " + user.LastName) : ""),
            new Claim(JwtClaimTypes.GivenName, user.FirstName  ?? ""),
            new Claim(JwtClaimTypes.FamilyName, user.LastName  ?? ""),
            new Claim(JwtClaimTypes.Email, user.Username  ?? ""),
            //new Claim("some_claim_you_want_to_see", user.Some_Data_From_User ?? ""),

            //roles
            new Claim(JwtClaimTypes.Role, "User")
            };
        }
    }
}