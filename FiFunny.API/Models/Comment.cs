using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiFunny.API.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PlaceId { get; set; }
        public string Description { get; set; }
        public bool Verified { get; set; }

        public DefaultUser user { get; set; }
    }
}