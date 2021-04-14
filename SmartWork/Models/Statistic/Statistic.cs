using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartWork.Models
{
    public class Statistic
    {
        public Statistic()
        {
            RoomStatistics = new HashSet<RoomStatistic>();
            VisitStatistics = new HashSet<VisitStatistic>();
        }
        public int Id { get; set; }
        public string StatisticName{ get; set; }
        public string StatisticDescription { get; set; }
        public ICollection<RoomStatistic> RoomStatistics { get; set; }
        public ICollection<VisitStatistic> VisitStatistics { get; set; }
    }
}
