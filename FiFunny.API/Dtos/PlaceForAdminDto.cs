using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiFunny.API.Dtos
{
    public class PlaceForAdminDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string Adres { get; set; }
        public string Phone { get; set; }
        public bool Verified { get; set; }
    }
}
