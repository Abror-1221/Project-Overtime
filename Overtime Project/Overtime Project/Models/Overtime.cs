using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime_Project.Models
{
    [Table("TB_M_Overtime")]
    public class Overtime
    {
        public int Id { get; set; }
        [ForeignKey("Person")]
        public string NIK { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string DescEmp { get; set; }
        public string DescHead { get; set; }
        public int TotalReimburse { get; set; }
        public int DayTypeId { get; set; }
        public int StatusId { get; set; }

        [JsonIgnore]
        public virtual Person Person { get; set; }
    }
}
