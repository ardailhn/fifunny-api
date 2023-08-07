﻿using FiFunny.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiFunny.API.Data
{
    public interface IAuthRepository
    {
        Task<User> Register(User user,string password);
        Task<User> Login(string email, string password);
        Task<bool> UserExists(string email);
    }
}
