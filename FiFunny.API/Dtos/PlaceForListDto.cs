using FiFunny.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiFunny.API.Dtos
{
    public class PlaceForListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhotoURL { get; set; }
        public string Description { get; set; }
        public bool Verified { get; set; }

        public Filter Filters { get; set; }

    }
}
