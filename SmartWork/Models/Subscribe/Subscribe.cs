using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartWork.Models
{
    public class Subscribe
    {
        public int id { get; set; }
        public string name { get; set; }
        public uint price { get; set; }
        public string desc { get; set; }
        public DateTime startSub { get; set; }
        public DateTime endSub { get; set; }
    }
}
