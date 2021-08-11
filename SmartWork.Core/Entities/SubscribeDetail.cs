namespace SmartWork.Core.Entities
{
    public class SubscribeDetail : Entity
    {
        public string SubscribeName { get; set; }
        public uint SubscribePrice { get; set; }
        public string SubscribeDescription { get; set; }
        public int OfficeId { get; set; }
        public virtual Office Office { get; set; }
    }
}