using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime_Project.Models
{
    public class Overtime
    {
        public int Id { get; set; }
        public string NIK { get; set; }
        public DateTime Date { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public string Description { get; set; }
        public int TotalReimburse { get; set; }
        public int TypeId { get; set; }
        public int StatusId { get; set; }
    }
}
