namespace SmartWork.Core.Entities
{
    public class MaterialEquipment : Entity
    {
        public string EquipmentName { get; set; }
        public int EquipmentCount { get; set; }
        public string EquipmentDesc { get; set; }
        public bool Available { get; set; }
        public int EquipmentId { get; set; }
    }
}