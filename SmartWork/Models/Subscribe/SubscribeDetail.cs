using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartWork.Models
{
    public class SubscribeDetail
    {
        public int id { get; set; }
        public int subId { get; set; }
        public int userId { get; set; }
        public int officeId { get; set; }
        public DateTime startSub { get; set; }
        public DateTime endSub { get; set; }
    }
}
