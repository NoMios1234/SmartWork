namespace SmartWork.Core.Entities
{
    public class VisitStatistic : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Data { get; set; }
        public string UserId { get; set; }
        public int StatisticId { get; set; }
        public virtual User User { get; set; }
        public virtual Statistic Statistic { get; set; }
    }
}
