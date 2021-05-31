using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Overtime_Project.Base;
using Overtime_Project.Context;
using Overtime_Project.Models;
using Overtime_Project.Repository.Data;
using Overtime_Project.ViewModels;
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
                           Password = a.Password
                       };
            return Ok(await data.ToListAsync());
        }
        [HttpPost("Register")] //BELUM ADA METHOD ROLLBACK DATABASE KETIKA ADA PENGISIAN TABLE YANG GAGAL!!!!!!!!! -Rangga
        public ActionResult Register(RegisterVM registerVM)
        {
            var checkNIKTerdaftar = overtimeContext.Person.Where(p => p.NIK == registerVM.NIK);
            if (checkNIKTerdaftar.Count() == 0)
            {

                var person = new Person
                {
                    NIK = registerVM.NIK,
                    FirstName = registerVM.FirstName,
                    LastName = registerVM.Email,
                    Phone = registerVM.Phone,
                    BirthDate = registerVM.BirthDate,
                    Salary = registerVM.Salary,
                    Email = registerVM.Email
                };
                overtimeContext.Person.Add(person);
                var addPerson = overtimeContext.SaveChanges();

                var account = new Account
                {
                    NIK = person.NIK,
                    Password = registerVM.Password
                };
                overtimeContext.Account.Add(account);
                var addAccount = overtimeContext.SaveChanges();

                var accountRole = new RoleAccount
                {
                    NIK = person.NIK,
                    RoleId = 3// default menjadi user saat pegawai baru didaftarkan
                };
                overtimeContext.RoleAccount.Add(accountRole);
                var addAccountRole = overtimeContext.SaveChanges();




                return Ok();
            }
            return NotFound();

        }
    }
}
