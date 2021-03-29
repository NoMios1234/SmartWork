using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartWork.Models
{
    public class SubscribeDetail
    {
        public int Id { get; set; }
        public int SubId { get; set; }
        public string UserId { get; set; }
        public int OfficeId { get; set; }
        public DateTime startSub { get; set; }
        public DateTime endSub { get; set; }
    }
}
