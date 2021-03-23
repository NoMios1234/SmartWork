using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartWork.Models.Statistic
{
    public class VisitStatistic
    {
        public int id { get; set; }
        public int userId { get; set; }
        public string statDesc { get; set; }
        public string data { get; set; }
    }
}
