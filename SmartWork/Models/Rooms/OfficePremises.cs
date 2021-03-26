using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartWork.Models
{
    public class OfficePremises
    {
        public int id { get; set; }
        public int officeId { get; set; }
        public int roomId { get; set; }
        public virtual Office Office { get; set; }
        public virtual Room Room { get; set; }
    }
}
