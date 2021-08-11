using System.Collections.Generic;

namespace SmartWork.Core.Entities
{
    public class Company : Entity
    {
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyPhoneNumber { get; set; }
        public string CompanyDescription { get; set; }
        public string PhotoFileName { get; set; }
        public virtual ICollection<Office> Offices { get; set; }
    } 
}