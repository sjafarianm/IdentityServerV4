using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer.Infrastructure.Cryptography;
using IdentityServerAPI.DataAccess.User;
using IdentityServerAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServerAPI.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IPasswordHash _passwordHash;
        private readonly IUserRepository _userRepository;
        public UserController(IPasswordHash passwordHash, IUserRepository userRepository)
        {
            _passwordHash = passwordHash;
            _userRepository = userRepository;
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
            return Ok("Test ok");
        }


    }
}