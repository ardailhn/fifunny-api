using FiFunny.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiFunny.API.Dtos
{
    public class PlaceForDetailDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Uyarilar { get; set; }
        public string Adres { get; set; }
        public int SehirId { get; set; }
        public int IlceId { get; set; }
        public string VideoURL { get; set; }
        public bool Verified { get; set; }
        public string sehiradi { get; set; }
        public string ilceadi { get; set; }
        public string VergiNumarasi { get; set; }

        public List<Photo> Photos { get; set; }

    }
}
