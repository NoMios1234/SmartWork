using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartWork.Models
{
    public class MaterialEquipment
    {
        public int id { get; set; }
        public string equipmentName { get; set; }
        public int equipmentCount { get; set; }
        public string equipmentDesc { get; set; }
        public bool available { get; set; }
        public int roomId { get; set; }
    }
}
