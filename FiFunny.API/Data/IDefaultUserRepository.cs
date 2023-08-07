using FiFunny.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiFunny.API.Data
{
    public interface IDefaultUserRepository
    {
        Task<DefaultUser> Register(DefaultUser defaultUser, string password);
        Task<DefaultUser> Login(string email, string password);
        Task<bool> UserExists(string email);
        DefaultUser GetDefaultUserById(int id);
    }
}
