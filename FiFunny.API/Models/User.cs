using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiFunny.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public int RoleId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public bool EmailVerified { get; set; }
        public string PhotoURL { get; set; }
        public string PhotoPublicId { get; set; }
        
    }
}
