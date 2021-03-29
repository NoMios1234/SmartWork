using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartWork.Models
{
    public class Office
    {
        public Office()
        {
            Subscribes = new HashSet<Subscribe>();
            Rooms = new HashSet<Room>();
        }
        public int Id { get; set; }
        public string OfficeName { get; set; }
        public string OfficeAddress { get; set; }
        public bool IsFavourite { get; set; }
        public virtual ICollection<Subscribe> Subscribes { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
