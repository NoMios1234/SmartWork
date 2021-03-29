using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartWork.Models
{
    public class MaterialEquipment
    {
        public int Id { get; set; }
        public string EquipmentName { get; set; }
        public int EquipmentCount { get; set; }
        public string EquipmentDesc { get; set; }
        public bool Available { get; set; }
        public int EquipmentId { get; set; }
    }
}
