using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartWork.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyPhoneNumber { get; set; }
        public string CompanyDescription { get; set; }
        public string PhotoFileName { get; set; }
        public virtual ICollection<Office> Offices { get; set; }
    }
    
}
