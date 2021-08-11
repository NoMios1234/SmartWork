using System.Collections.Generic;

namespace SmartWork.Core.Entities
{
    public class Statistic : Entity
    {
        public string StatisticName{ get; set; }
        public string StatisticDescription { get; set; }
        public virtual ICollection<RoomStatistic> RoomStatistics { get; set; }
        public virtual ICollection<VisitStatistic> VisitStatistics { get; set; }
    }
}