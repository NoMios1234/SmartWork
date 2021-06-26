using System.Collections.Generic;

namespace SmartWork.Core.Models
{
    public class Office
    {
        public Office()
        {
            Subscribes = new HashSet<Subscribe>();
            Rooms = new HashSet<Room>();
        }
        public int Id { get; set; }
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