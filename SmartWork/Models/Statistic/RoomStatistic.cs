using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartWork.Models.Statistic
{
    public class RoomStatistic
    {
        public int id { get; set; }
        public int roomId { get; set; }
        public string statDesc { get; set; }
        public string data { get; set; }
    }
}
