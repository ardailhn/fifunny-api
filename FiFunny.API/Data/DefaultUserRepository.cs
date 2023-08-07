using FiFunny.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiFunny.API.Data
{
    public class DefaultUserRepository : IDefaultUserRepository
    {
        private DataContext _dataContext;

        public DefaultUserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<DefaultUser> Login(string email, string password)
        {
            var user = await _dataContext.DefaultUser.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
            {
                return null;
            }

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public async Task<DefaultUser> Register(DefaultUser user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _dataContext.DefaultUser.AddAsync(user);
            await _dataContext.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists(string email)
        {
            if (await _dataContext.DefaultUser.AnyAsync(x => x.Email == email))
            {
                return true;
            }
            return false;
        }

        public DefaultUser GetDefaultUserById(int id)
        {
            var user = _dataContext.DefaultUser.FirstOrDefault(u => u.Id == id);
            return user;
        }
    }
}
