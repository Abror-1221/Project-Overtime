using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Overtime_Project.Base;
using Overtime_Project.Context;
using Overtime_Project.Models;
using Overtime_Project.Repository.Data;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository accountRepository;
        private readonly OvertimeContext overtimeContext;
        public IConfiguration _configuration;
        public AccountController(AccountRepository accountRepository, OvertimeContext overtimeContext, IConfiguration configuration) : base(accountRepository)
        {
            this.accountRepository = accountRepository;
            this.overtimeContext = overtimeContext;
            this._configuration = configuration;
        }
       
        //wadaw1
        // wadawwww
        //wadaww 3
        [HttpGet("UserData")]
        public async Task<ActionResult> ViewDataAll()
        {
            var data = from p in overtimeContext.Person
                       join a in overtimeContext.Account on p.NIK equals a.NIK
                       join ar in overtimeContext.RoleAccount on a.NIK equals ar.NIK
                       join r in overtimeContext.Role on ar.RoleId equals r.Id
                       join o in overtimeContext.Overtime on p.NIK equals o.NIK
                       join s in overtimeContext.Status on o.StatusId equals s.Id
                       join k in overtimeContext.Kind on o.TypeId equals k.Id
                       select new
                       {
                           NIK = p.NIK,
                           FirstName = p.FirstName,
                           LastName = p.LastName,
                           Role = r.Name,
                           RoleId = r.Id,
                           Phone = p.Phone,
                           BirthDate = p.BirthDate,
                           Salary = p.Salary,
                           Email = p.Email,
                           OvertimeId = o.Id,
                           Type = k.Name,
                           TypeId = k.Id,
                           Status = s.Name,
                           StatusId = s.Id
                       };
            return Ok(await data.ToListAsync());
        }
    }
}
