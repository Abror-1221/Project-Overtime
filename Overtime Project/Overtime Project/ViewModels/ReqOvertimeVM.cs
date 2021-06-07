using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime_Project.ViewModels
{
    public class ReqOvertimeVM
    {
        public int Id { get; set; }
        public string NIK { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string DescEmp { get; set; }
        public string DescHead { get; set; }
        public int TotalReimburse { get; set; }
        public int DayTypeId { get; set; }
        public string DayTypeName { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
    }
}
