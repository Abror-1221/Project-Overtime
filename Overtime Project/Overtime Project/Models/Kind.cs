using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime_Project.Models
{
    [Table("TB_M_Kind")]
    public class Kind
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<Overtime> Overtimes { get; set; }
    }
}
