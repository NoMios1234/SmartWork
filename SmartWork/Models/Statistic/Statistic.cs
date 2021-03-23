using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartWork.Models.Statistic
{
    public class Statistic
    {
        public int id { get; set; }
        public string statDesc { get; set; }
        public List<RoomStatistic> roomStatistics { get; set; }
        public List<VisitStatistic> visitStatistics { get; set; }
    }
}
