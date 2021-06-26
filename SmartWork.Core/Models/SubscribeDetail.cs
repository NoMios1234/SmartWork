namespace SmartWork.Core.Models
{
    public class SubscribeDetail
    {
        public int Id { get; set; }
        public string SubscribeName { get; set; }
        public uint SubscribePrice { get; set; }
        public string SubscribeDescription { get; set; }
        public int OfficeId { get; set; }
        public virtual Office Office { get; set; }
    }
}