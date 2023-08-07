using FiFunny.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiFunny.API.Data
{
    public class UserRepository : IUserRepository
    {

        private DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public List<Place> GetPlacesByUserId(int id)
        {
            var places = _context.Places.Where(p => p.UserId == id).ToList();
            return places;
        }

        public User GetUserById(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            return user;
        }
    }
}
