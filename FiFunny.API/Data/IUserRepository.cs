using FiFunny.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiFunny.API.Data
{
    public interface IUserRepository
    {
        User GetUserById(int id);
        List<Place> GetPlacesByUserId(int id);

    }
}
