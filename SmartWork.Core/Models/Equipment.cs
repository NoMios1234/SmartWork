using System.Collections.Generic;

namespace SmartWork.Core.Models
{
    public class Equipment
    {
        public Equipment()
        {
            MaterialEquipments = new HashSet<MaterialEquipment>();
            TechnicalEquipments = new HashSet<TechnicalEquipment>();
        }

        public int Id { get; set; }
        public string EquipmentDesc { get; set; }
        public int RoomId { get; set; }
        public virtual ICollection<MaterialEquipment> MaterialEquipments { get; set; }
        public virtual ICollection<TechnicalEquipment> TechnicalEquipments { get; set; }
    }
}