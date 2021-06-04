using Overtime_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationOvertime.Base;

namespace WebApplicationOvertime.Repository.Data
{
    public class AccountRepository : GeneralRepository<Account, string>
    {
        public AccountRepository(Address address, string request = "Account/") : base(address, request)
        {

        }


    }
}
