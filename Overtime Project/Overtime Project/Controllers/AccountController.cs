using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Overtime_Project.Base;
using Overtime_Project.Context;
using Overtime_Project.HashingPassword;
using Overtime_Project.Models;
using Overtime_Project.Repository.Data;
using Overtime_Project.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
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

        [HttpGet("Profile/{NIK}")]
        public ActionResult ViewData(string NIK)
        {
            var data = from p in overtimeContext.Person
                       join a in overtimeContext.Account on p.NIK equals a.NIK
                       join ar in overtimeContext.RoleAccount on a.NIK equals ar.NIK
                       join r in overtimeContext.Role on ar.RoleId equals r.Id
                       where p.NIK == NIK
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

            return Ok(data);
        }

        [HttpGet("UserData")]
        public async Task<ActionResult> ViewDataAll()
        {
            var data = from p in overtimeContext.Person
                       join a in overtimeContext.Account on p.NIK equals a.NIK
                       join ar in overtimeContext.RoleAccount on a.NIK equals ar.NIK
                       join r in overtimeContext.Role on ar.RoleId equals r.Id
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

        [HttpPost("ForgotPassword")]
        public ActionResult SendMail(ChangePasswordVM change)
        {
            if (change.Email != null)
            {
                if(change.Email != "")
                {
                    string nik = null;
                    IEnumerable<Person> person = overtimeContext.Person.ToList();
                    foreach (Person p in person)
                    {
                        if (p.Email == change.Email)
                        {
                            nik = p.NIK;
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }

                    if (nik != null)
                    {
                        var cekPerson = overtimeContext.Person.Find(nik);
                        var cekAccount2 = overtimeContext.Account.Find(nik);
                        string password = RandomString();
                        cekAccount2.Password = Hashing.HashPassword(password);
                        overtimeContext.Entry(cekAccount2).State = EntityState.Modified;
                        overtimeContext.SaveChanges();

                        DateTime d = DateTime.Now;
                        MailMessage mm = new MailMessage();
                        mm.From = new MailAddress("developit9@gmail.com");
                        mm.To.Add(new MailAddress(change.Email));
                        mm.Subject = $"[RESET PASSWORD REQUEST] {d.ToString("dd-MM-yyyy")}";
                        mm.Body = $"Hello {cekPerson.FirstName} {cekPerson.LastName}.\nYour new password is {password}\nHave a nice day...";
                        mm.IsBodyHtml = false;
                        SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                        smtp.Port = 587;
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential("developit9@gmail.com", "Sembilan!@9");
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Send(mm);
                        return Ok("Email sent...");
                    }
                    else
                    {
                        return StatusCode(400, new { status = HttpStatusCode.Forbidden, message = "Error : Email is not registered..." });
                    }
                }
                else
                {
                    return StatusCode(400, new { status = HttpStatusCode.Forbidden, message = "Error : No Email input..." });
                }
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.Forbidden, message = "Error : No Email input..." });
            }
        }

        public string RandomString()
        {
            int length = 7;
            StringBuilder str_build = new StringBuilder();
            Random random = new Random();
            char letter;
            for (int i = 0; i < length; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                str_build.Append(letter);
            }
            return str_build.ToString();
        }


    }
}
