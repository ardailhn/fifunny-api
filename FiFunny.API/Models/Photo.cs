using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiFunny.API.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public int PlaceId { get; set; }
        public bool IsMain { get; set; }
        public string URL { get; set; }
        public string PublicId { get; set; }

        public Place Place { get; set; }
    }
}
