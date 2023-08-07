using FiFunny.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiFunny.API.Data
{
    public interface IAdminRepository
    {
        void Delete<T>(T entity) where T : class;
        bool SaveAll();

        Task<Notification> SendMessage(Notification noti);
        Task<Admin> Register(Admin admin, string password);
        Task<Admin> Login(string userName, string password);
        Task<bool> UserExists(string userName);
        List<Notification> GetNotification();
        List<Place> GetPlaces();
        List<Place> GetPlacesList();
    }
}
