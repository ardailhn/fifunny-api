using FiFunny.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiFunny.API.Data
{
    public class AdminRepository:IAdminRepository
    {

        private DataContext _dataContext;

        public AdminRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Delete<T>(T entity) where T : class
        {
            _dataContext.Remove(entity);
        }

        public async Task<Admin> Login(string userName, string password)
        {
            var admin = await _dataContext.Admins.FirstOrDefaultAsync(x => x.UserName == userName);
            if (admin == null)
            {
                return null;
            }

            if (!VerifyPasswordHash(password, admin.PasswordHash, admin.PasswordSalt))
            {
                return null;
            }

            return admin;
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

        public async Task<Admin> Register(Admin admin, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            admin.PasswordHash = passwordHash;
            admin.PasswordSalt = passwordSalt;

            await _dataContext.Admins.AddAsync(admin);
            await _dataContext.SaveChangesAsync();

            return admin;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<Notification> SendMessage(Notification noti)
        {
            await _dataContext.Notifications.AddAsync(noti);
            await _dataContext.SaveChangesAsync();

            return noti;
        }

        public async Task<bool> UserExists(string userName)
        {
            if (await _dataContext.Admins.AnyAsync(x => x.UserName == userName))
            {
                return true;
            }
            return false;
        }

        public List<Notification> GetNotification()
        {
            return _dataContext.Notifications.ToList();
        }

        public List<Place> GetPlaces()
        {
            var places = _dataContext.Places.Where(p => p.Verified == false).ToList();
            return places;
        }

        public List<Place> GetPlacesList()
        {
            var places = _dataContext.Places.ToList();
            return places;
        }

        public bool SaveAll()
        {
            return _dataContext.SaveChanges() > 0;
        }

    }
}
