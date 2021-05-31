using Overtime_Project.Context;
using Overtime_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime_Project.Repository.Data
{
    public class AccountRepository : GeneralRepository<OvertimeContext, Account, string>
    {
        public AccountRepository(OvertimeContext conn) : base(conn)
        {

        }
    }
}
