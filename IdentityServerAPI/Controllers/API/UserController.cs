using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer.Infrastructure.Cryptography;
using IdentityServer.Infrastructure.Logging;
using IdentityServerAPI.DataAccess.User;
using IdentityServerAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IdentityServerAPI.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IPasswordHash _passwordHash;
        private readonly IUserRepository _userRepository;
        private readonly IdentityServer.Infrastructure.Logging.ILogger _logger;
        public UserController(IPasswordHash passwordHash, IUserRepository userRepository, IdentityServer.Infrastructure.Logging.ILogger logger)
        {
            _passwordHash = passwordHash;
            _userRepository = userRepository;
            _logger = logger;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateUser(CreateUserModel model)
        {
            var pass = _passwordHash.Encrypt(model.Password);

            await _userRepository.AddAsync(new UserModel
            {
                FirstName = model.FirstName,
                IsActive = true,
                LastName = model.LastName,
                Password = Convert.ToBase64String(pass.Signature),
                PasswordSalt= Convert.ToBase64String(pass.Salt),
                Username = model.Username,
                UserId = model.UserId
            });
            return Ok("created");
        }


        [HttpGet]
        [Route("test")]
        public async Task<IActionResult> Test()
        {
            _logger.Logger("LogTest").Info("test method");
            return Ok("Test ok");
        }


    }
}