using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartWork.Models 
{ 
    public class RoomStatistic
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string Description { get; set; }
        public string Data { get; set; }    
        public int StatisticId { get; set; }
       
    }
}
