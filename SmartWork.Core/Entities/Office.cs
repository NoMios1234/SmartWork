using System.Collections.Generic;

namespace SmartWork.Core.Entities
{
    public class Office : Entity
    {
        public string OfficeName { get; set; }
        public string OfficeAddress { get; set; }
        public string OfficePhoneNumber { get; set; }
        public bool IsFavourite { get; set; }
        public int CompanyId { get; set; }
        public string PhotoFileName { get; set; }
        public virtual ICollection<Subscribe> Subscribes { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}