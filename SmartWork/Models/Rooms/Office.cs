using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartWork.Models
{
    public class Office
    {
        public int id { get; set; }
        public string officeName { get; set; }
        public string officeAddress { get; set; }
        public bool isFavourite { get; set; }
        public int subscribeId { get; set; }
        public virtual Subscribe Subscribe { get; set; }
        public virtual List<RoomInfo> Rooms { get; set; }
    }
}
