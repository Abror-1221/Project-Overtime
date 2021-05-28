using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime_Project.Models
{
    [Table("TB_M_Account")]
    public class Account
    {
        [Key]
        [ForeignKey("Person")]
        public string NIK { get; set; }
        public string Password { get; set; }
        public virtual Person Person { get; set; }

        public virtual ICollection<RoleAccount> RoleAccounts { get; set; }
        public Account() { }

    }
}
