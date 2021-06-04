using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime_Project.Models
{
    [Table("TB_M_Person")]
    public class Person
    {
        [Key]
        public string NIK { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public int Salary { get; set; }
        public int IsDeleted { get; set; }

        public Person() { }
        [JsonIgnore]
        public virtual ICollection<Overtime> Overtime { get; set; }
        [JsonIgnore]
        public virtual Account Account { get; set; }
    }
}
