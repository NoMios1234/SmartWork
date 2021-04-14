using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartWork.Models
{
    public class Subscribe
    {
        public int Id { get; set; }
        public string SubscribeName { get; set; }
        public string SubscribeDescription { get; set; }
        public DateTime StartSubscribe { get; set; }
        public DateTime EndSubscribe { get; set; }
        public string UserId { get; set; }
        public int SubscribeDetailId { get; set; }
        public virtual User User { get; set; }
        public virtual SubscribeDetail SubscribeDetail { get; set; }
        
    }
}
