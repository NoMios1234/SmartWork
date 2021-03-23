using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartWork.Models
{
    public class Office
    {
        public int id { get; set; }
        public string roomName { get; set; }
        public string roomAddress { get; set; }
        public uint subPrice { get; set; } // price for visit
        public bool isFavourite { get; set; }
        public Room Room { get; set; }
    }
}
