using System.Collections.Generic;

namespace SmartWork.Core.Entities
{
    public class Equipment : Entity
    {
        public string EquipmentDesc { get; set; }
        public int RoomId { get; set; }
        public virtual ICollection<MaterialEquipment> MaterialEquipments { get; set; }
        public virtual ICollection<TechnicalEquipment> TechnicalEquipments { get; set; }
    }
}