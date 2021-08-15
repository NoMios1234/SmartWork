using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWork.Core.ViewModels.SubscribeViewModels
{
    public class AddSubscribeViewModel
    {
        public string SubscribeName { get; set; }
        public string SubscribeDescription { get; set; }
        public DateTime StartSubscribe { get; set; }
        public DateTime EndSubscribe { get; set; }
        public string UserId { get; set; }
        public int SubscribeDetailId { get; set; }
    }
}
