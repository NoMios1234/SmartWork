using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartWork.Models
{
    public class Subscribe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public uint Price { get; set; }
        public string Desc { get; set; }
        public virtual Office Office { get; set; }
    }
}
