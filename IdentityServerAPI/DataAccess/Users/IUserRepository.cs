using IdentityServerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAPI.DataAccess.User
{
    public interface IUserRepository 
    {
        Task<UserModel> GetByIdAsync(string userId);
        Task<UserModel> GetByUsernameAsync(string userId);
        Task<bool> AddAsync(UserModel item);
        Task DeleteAsync(string userId);
    }
}
