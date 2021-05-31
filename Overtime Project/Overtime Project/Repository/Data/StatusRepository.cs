using Overtime_Project.Context;
using Overtime_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime_Project.Repository.Data
{
    public class StatusRepository : GeneralRepository<OvertimeContext, Status, int>
    {
        public StatusRepository(OvertimeContext conn) : base(conn)
        {

        }
    }
}
