using FiFunny.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiFunny.API.Dtos
{
    public class CommentsForListDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PlaceId { get; set; }
        public string Description { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public bool Verified { get; set; }

        public DefaultUser user { get; set; }
    }
}
