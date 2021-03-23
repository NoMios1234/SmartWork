using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartWork.Models
{
    public class Company
    {
        public int id { get; set; }
        public string companyName { get; set; }
        public Room Room { get; set; }
    }
}
