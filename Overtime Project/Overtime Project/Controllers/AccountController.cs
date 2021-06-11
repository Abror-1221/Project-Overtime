using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Overtime_Project.Base;
using Overtime_Project.Context;
using Overtime_Project.HashingPassword;
using Overtime_Project.Models;
using Overtime_Project.Repository.Data;
using Overtime_Project.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Overtime_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
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
                           Gender = p.Gender,
                           Role = r.Name,
                           RoleId = r.Id,
                           Phone = p.Phone,
                           BirthDate = p.BirthDate,
                           Salary = p.Salary,
                           Email = p.Email,
                           OvertimeHour = p.OvertimeHour
                       };
            return Ok(data);
        }

        // [Authorize(Roles = "Admin,Head")]
        [HttpGet("UserData")]
        public async Task<ActionResult> ViewDataAll()
        {
            var data = from p in overtimeContext.Person
                       join a in overtimeContext.Account on p.NIK equals a.NIK
                       join ar in overtimeContext.RoleAccount on a.NIK equals ar.NIK
                       join r in overtimeContext.Role on ar.RoleId equals r.Id
                       where r.Name == "Employee" && p.IsDeleted == 0
                       select new
                       {
                           NIK = p.NIK,
                           FirstName = p.FirstName,
                           LastName = p.LastName,
                           Gender = p.Gender,
                           Role = r.Name,
                           RoleId = r.Id,
                           Phone = p.Phone,
                           BirthDate = p.BirthDate,
                           Salary = p.Salary,
                           Email = p.Email,
                           OvertimeHour = p.OvertimeHour
                       };

            return Ok(await data.ToListAsync());
        }

        [HttpPost("Register")] //BELUM ADA METHOD ROLLBACK DATABASE KETIKA ADA PENGISIAN TABLE YANG GAGAL!!!!!!!!! -Rangga
       // [Authorize(Roles = "Admin")]
        public ActionResult Register(RegisterVM registerVM)
        {
            var checkNIKTerdaftar = overtimeContext.Person.Where(p => p.NIK == registerVM.NIK);
            if (checkNIKTerdaftar.Count() == 0)
            {
                var checkEmailTerdaftar = overtimeContext.Person.Where(p => p.Email == registerVM.Email);
                if (checkEmailTerdaftar.Count() == 0)
                {
                    var person = new Person
                    {
                        NIK = registerVM.NIK,
                        FirstName = registerVM.FirstName,
                        LastName = registerVM.LastName,
                        Gender = registerVM.Gender,
                        Phone = registerVM.Phone,
                        BirthDate = registerVM.BirthDate,
                        Salary = registerVM.Salary,
                        Email = registerVM.Email,
                        OvertimeHour = 14,
                        IsDeleted = 0
                    };
                    overtimeContext.Person.Add(person);
                    var addPerson = overtimeContext.SaveChanges();

                    var account = new Account
                    {
                        NIK = person.NIK,
                        Password = Hashing.HashPassword(registerVM.Password)
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
                else
                {
                    return StatusCode(403, new { status = HttpStatusCode.Forbidden, message = "Error : Email is already registered..." });
                }
            }
            else
            {
                return StatusCode(403, new {status = HttpStatusCode.Forbidden, message = "Error : NIK is already registered..." });
            }
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
                        return StatusCode(200, new { status = HttpStatusCode.OK, message = "Requested for reset password" });
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

        [HttpPost("Login")]
        public ActionResult Index(LogInVM logInVM)
        {
            var myPerson = overtimeContext.Person.FirstOrDefault(u => u.Email == logInVM.Email);

            if (myPerson != null)
            {
                var myAccount = overtimeContext.Account.FirstOrDefault(u => u.NIK == myPerson.NIK);
                var accountRole = overtimeContext.RoleAccount.Where(ar => ar.NIK == myAccount.NIK).ToList();

                if (myAccount != null && Hashing.ValidatePassword(logInVM.Password, myAccount.Password))    //User was found
                {
                    List<Claim> claims = new List<Claim>();
                    foreach (var item in accountRole.Where(n => n.NIK == myAccount.NIK))
                    {
                        claims.Add(new Claim(ClaimTypes.Role, item.Role.Name));
                    }

                    claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()));
                    claims.Add(new Claim("NIK", myPerson.NIK));
                    claims.Add(new Claim("First Name", myPerson.FirstName));
                    claims.Add(new Claim("Last Name", myPerson.LastName));
                    claims.Add(new Claim("Email", logInVM.Email));

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddMinutes(10), signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else return NotFound("Wrong EMAIL or Password!");
            }
            else return NotFound("Wrong EMAIL or Password!");
        }

        [HttpPost("Changepass")]
       // [Authorize(Roles = "Employee")]
        public ActionResult Updatepassword(ChangePasswordVM changePasswordVM)
        {
            var person = overtimeContext.Person.FirstOrDefault(p => p.Email == changePasswordVM.Email);
            if (person != null)
            {


                var tesNIK = overtimeContext.Account.FirstOrDefault(u => u.NIK == person.NIK);

                if (tesNIK != null && Hashing.ValidatePassword(changePasswordVM.Password, tesNIK.Password))
                {

                    tesNIK.Password = Hashing.HashPassword(changePasswordVM.NewPassword);
                    overtimeContext.Entry(tesNIK).State = EntityState.Modified;
                    overtimeContext.SaveChanges();
                    return Ok();

                }
                else return NotFound();

            }
            else return NotFound("Email Tidak Terdaftar!");

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
