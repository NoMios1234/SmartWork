using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartWork.Models
{
    public class Equipment
    {
        public int id { get; set; }
        public string equipmentDesc { get; set; }
        public int materialEquipmentId { get; set; }
        public int technicalEquipmentId { get; set; }
        public virtual List<MaterialEquipment> materialEquipment { get; set; }
        public virtual List<TechnicalEquipment> technicalEquipment { get; set; }
    }
}
