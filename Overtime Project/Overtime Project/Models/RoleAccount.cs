using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime_Project.Models
{
    [Table("TB_T_RoleAccount")]
    public class RoleAccount
    {
        public string NIK { get; set; }
        [JsonIgnore]
        public virtual Account Account { get; set; }
        public int RoleId { get; set; }
        [JsonIgnore]
        public virtual Role Role { get; set; }
    }
}
