using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartWork.Models
{
    public class RoomInfo
    {
        public int id { get; set; }
        public string roomName { get; set; }
        public int roomNumber { get; set; }
        public string companyName { get; set; }
        public int temperature { get; set; }
        public int light { get; set; } // in lumens
        public double square { get; set; }
        public int equipmentId { get; set; }
        public int officeId { get; set; }
    }
}
