using Dapper;
using IdentityServerAPI.DataAccess.User;
using IdentityServerAPI.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAPI.DataAccess.Users
{
    public class UserRepository : IUserRepository
    {
        private IConfiguration _configuration;
        private string _connectionString;
        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<bool> AddAsync(UserModel item)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.ExecuteAsync("Users_Add", 
                    new {
                        item.Username,
                        item.UserId,
                        item.Password,
                        item.PasswordSalt,
                        item.FirstName,
                        item.LastName,
                    },
                    commandType: CommandType.StoredProcedure);
            }
            return true;
        }

        public async Task DeleteAsync(string userId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.ExecuteAsync("Users_DeleteByUserId", new { userId },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<UserModel> GetByIdAsync(string userId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                var result = await conn.QueryFirstAsync<UserModel>("Users_GetUserByUserId", new { userId },
                    commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<UserModel> GetByUsernameAsync(string username)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                var result = await conn.QueryFirstAsync<UserModel>("Users_GetUserByUsername", new { username },
                    commandType: CommandType.StoredProcedure);
                return result;
            }
        }

    }
}
