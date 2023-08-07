using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiFunny.API.Models
{
    public class about
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string text { get; set; }
        public string PhotoURL { get; set; }
        public string PhotoPublicId { get; set; }
    }
}
