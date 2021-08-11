using System.Collections.Generic;

namespace SmartWork.Core.Entities
{
    public class Room : Entity
    {
        public string RoomName { get; set; }
        public int RoomNumber { get; set; }
        public string CompanyName { get; set; }
        public int Temperature { get; set; }
        public int Light { get; set; } // in lumens
        public double Square { get; set; }
        public int OfficeId { get; set; }
        public string PhotoFileName { get; set; }
        public virtual ICollection<Equipment> Equipments { get; set; }
    }
}