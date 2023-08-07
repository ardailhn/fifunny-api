using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiFunny.API.Models
{
    public class Place
    {
        public Place()
        {
            Photos = new List<Photo>();
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Uyarilar { get; set; }
        public string Adres { get; set; }
        public int SehirId { get; set; }
        public int IlceId { get; set; }
        public int DayId { get; set; }
        public string AcilisSaati { get; set; }
        public string KapanisSaati { get; set; }
        public string VideoURL { get; set; }
        public bool Verified { get; set; }
        public string VergiNumarasi { get; set; }

        public Filter Filters { get; set; }
        public List<Photo> Photos { get; set; }
        public User User { get; set; }
        public Category Category { get; set; }

    }
}
