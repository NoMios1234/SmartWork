namespace SmartWork.Core.Models
{
    public class RoomStatistic
    {
        public int Id { get; set; }
        public string RoomStatisticName { get; set; }
        public string Description { get; set; }
        public string Data { get; set; }    
        public int RoomId { get; set; }
        public int StatisticId { get; set; }
        public virtual Room Room { get; set; }
        public virtual Statistic Statistic { get; set; }

    }
}