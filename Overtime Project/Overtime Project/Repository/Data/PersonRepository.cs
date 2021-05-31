using Overtime_Project.Context;
using Overtime_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime_Project.Repository.Data
{
    public class PersonRepository : GeneralRepository<OvertimeContext, Person, string>
    {
        public PersonRepository(OvertimeContext conn) : base(conn)
        {

        }
    }
}
