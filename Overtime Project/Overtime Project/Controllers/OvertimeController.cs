using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Overtime_Project.Base;
using Overtime_Project.Context;
using Overtime_Project.EmailHandling;
using Overtime_Project.Models;
using Overtime_Project.Repository.Data;
using Overtime_Project.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Overtime_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OvertimeController : BaseController<Overtime, OvertimeRepository, int>
    {
        private readonly OvertimeRepository overtimeRepository;
        private readonly OvertimeContext overtimeContext;
        private readonly SendMail sendMail = new SendMail();
        public IConfiguration _configuration;
        public OvertimeController(OvertimeRepository overtimeRepository, OvertimeContext overtimeContext, IConfiguration configuration) : base(overtimeRepository)
        {
            this.overtimeRepository = overtimeRepository;
            this.overtimeContext = overtimeContext;
            this._configuration = configuration;
        }


        [HttpGet("OvertimeData/{NIK}")]
        public ActionResult ViewDataOvertime(string NIK)
        {
            var data = from p in overtimeContext.Person
                       join a in overtimeContext.Account on p.NIK equals a.NIK
                       join ar in overtimeContext.RoleAccount on a.NIK equals ar.NIK
                       join r in overtimeContext.Role on ar.RoleId equals r.Id
                       join o in overtimeContext.Overtime on p.NIK equals o.NIK
                       join s in overtimeContext.Status on o.StatusId equals s.Id
                       join k in overtimeContext.DayType on o.DayTypeId equals k.Id
                       where o.NIK == NIK && p.IsDeleted == 0 && s.Id == 1
                       select new
                       {
                           Id = o.Id,
                           NIK = o.NIK,
                           Date = o.Date,
                           StartTime = o.StartTime,
                           EndTime = o.EndTime,
                           DescEmp = o.DescEmp,
                           DescHead = o.DescHead,
                           DayTypeId = o.DayTypeId,
                           DayTypeName = k.Name,
                           StatusId = o.StatusId,
                           StatusName = s.Name,
                       };
            return Ok(data.ToList());
        }
        [HttpGet("OvertimeDataHistory/{NIK}")]
        public ActionResult ViewDataOvertimeHisotry(string NIK)
        {
            var data = from p in overtimeContext.Person
                       join a in overtimeContext.Account on p.NIK equals a.NIK
                       join ar in overtimeContext.RoleAccount on a.NIK equals ar.NIK
                       join r in overtimeContext.Role on ar.RoleId equals r.Id
                       join o in overtimeContext.Overtime on p.NIK equals o.NIK
                       join s in overtimeContext.Status on o.StatusId equals s.Id
                       join k in overtimeContext.DayType on o.DayTypeId equals k.Id
                       where o.NIK == NIK && p.IsDeleted == 0 && s.Id !=1
                       select new
                       {
                           Id = o.Id,
                           NIK = o.NIK,
                           Date = o.Date,
                           StartTime = o.StartTime,
                           EndTime = o.EndTime,
                           DescEmp = o.DescEmp,
                           DescHead = o.DescHead,
                           DayTypeId = o.DayTypeId,
                           DayTypeName = k.Name,
                           StatusId = o.StatusId,
                           StatusName = s.Name,
                       };
            return Ok(data.ToList());
        }

        [HttpGet("OvertimeDataAll")]
        public async Task<ActionResult> ViewDataOvertimeAll()
        {
            var data = from p in overtimeContext.Person
                       join a in overtimeContext.Account on p.NIK equals a.NIK
                       join ar in overtimeContext.RoleAccount on a.NIK equals ar.NIK
                       join r in overtimeContext.Role on ar.RoleId equals r.Id
                       join o in overtimeContext.Overtime on p.NIK equals o.NIK
                       join s in overtimeContext.Status on o.StatusId equals s.Id
                       join k in overtimeContext.DayType on o.DayTypeId equals k.Id
                       where p.IsDeleted == 0 && s.Id == 1
                       select new
                       {
                           Id = o.Id,
                           NIK = o.NIK,
                           FirstName = p.FirstName,
                           LastName = p.LastName,
                           Email = p.Email,
                           Phone = p.Phone,
                           Date = o.Date,
                           StartTime = o.StartTime,
                           EndTime = o.EndTime,
                           DescEmp = o.DescEmp,
                           DescHead = o.DescHead,
                           TotalReimburse = o.TotalReimburse,
                           DayTypeId = o.DayTypeId,
                           DayTypeName = k.Name,
                           StatusId = o.StatusId,
                           StatusName = s.Name,
                       };
            return Ok(await data.ToListAsync());
        }
        [HttpGet("OvertimeDataAllHistory")]
        public async Task<ActionResult> ViewDataOvertimeAllHistory()
        {
            var data = from p in overtimeContext.Person
                       join a in overtimeContext.Account on p.NIK equals a.NIK
                       join ar in overtimeContext.RoleAccount on a.NIK equals ar.NIK
                       join r in overtimeContext.Role on ar.RoleId equals r.Id
                       join o in overtimeContext.Overtime on p.NIK equals o.NIK
                       join s in overtimeContext.Status on o.StatusId equals s.Id
                       join k in overtimeContext.DayType on o.DayTypeId equals k.Id
                       where p.IsDeleted == 0 && s.Id != 1
                       select new
                       {
                           Id = o.Id,
                           NIK = o.NIK,
                           FirstName = p.FirstName,
                           LastName = p.LastName,
                           Email = p.Email,
                           Phone = p.Phone,
                           Date = o.Date,
                           StartTime = o.StartTime,
                           EndTime = o.EndTime,
                           DescEmp = o.DescEmp,
                           DescHead = o.DescHead,
                           TotalReimburse = o.TotalReimburse,
                           DayTypeId = o.DayTypeId,
                           DayTypeName = k.Name,
                           StatusId = o.StatusId,
                           StatusName = s.Name,
                       };
            return Ok(await data.ToListAsync());
        }

        [HttpPost("ReqOvertime/{NIK}")]
      //  [Authorize(Roles = "Employee")]
        public ActionResult RequestOvertime(string NIK,ReqOvertimeVM reqOvertimeVM)
        {
            var totalReimburse = 0;
            var hour = 0;
            //var myHead = from p in overtimeContext.Person
            //             join a in overtimeContext.Account on p.NIK equals a.NIK
            //             join ra in overtimeContext.RoleAccount on a.NIK equals ra.NIK
            //             join r in overtimeContext.Role on ra.RoleId equals r.Id
            //             where r.Name=="Head"
            //             select new
            //             {
            //                 Email = p.Email
            //             };
            var myHead = overtimeContext.RoleAccount.FirstOrDefault(i => i.RoleId == 2);
            //var myHeadAcc = overtimeContext.Account.FirstOrDefault(u => u.NIK == myHead.NIK);
            var myHeadPerson = overtimeContext.Person.FirstOrDefault(u => u.NIK == myHead.NIK);

            var myPerson = overtimeContext.Person.FirstOrDefault(u => u.NIK == NIK);
            if (myPerson != null)
            {

                hour = (int)Math.Ceiling(Convert.ToDouble((reqOvertimeVM.EndTime - reqOvertimeVM.StartTime).TotalHours));
                if (reqOvertimeVM.StartTime.DayOfWeek == DayOfWeek.Sunday || reqOvertimeVM.StartTime.DayOfWeek == DayOfWeek.Saturday)
                {
                    if (hour <= 8)
                    {
                        totalReimburse += (int)Math.Ceiling((double)hour * 2 / 173 * myPerson.Salary);
                    }
                    else
                    {
                        totalReimburse += (int)Math.Ceiling(8.0 * 2 / 173 * (double)myPerson.Salary);
                        totalReimburse += (int)Math.Ceiling(3.0 / 173 * (double)myPerson.Salary);
                        for (int i=2 ; i <= hour-8 ; i++)
                        {
                            totalReimburse += (int)Math.Ceiling(4.0 / 173 * (double)myPerson.Salary);
                        }
                    }
                }
                else if (reqOvertimeVM.StartTime.DayOfWeek != DayOfWeek.Sunday || reqOvertimeVM.StartTime.DayOfWeek != DayOfWeek.Saturday)
                {
                    totalReimburse = (int)Math.Ceiling(1.5 / 173 * myPerson.Salary);
                    for (int i = 1; i <= hour - 1; i++)
                    {
                        totalReimburse += (int)Math.Ceiling(2.0 / 173 * myPerson.Salary); ;
                    }
                }
                //else if (reqOvertimeVM.DayTypeId == 3)
                //{
                //    if (hour <= 5)
                //    {
                //        totalReimburse += (int)Math.Ceiling((double)hour * 2 / 173 * (double)myPerson.Salary);
                //    }
                //    else
                //    {
                //        totalReimburse += (int)Math.Ceiling(5.0 * 2 / 173 * (double)myPerson.Salary);
                //        totalReimburse += (int)Math.Ceiling(3.0 / 173 * (double)myPerson.Salary);
                //        for (int i = 2; i <= hour - 5; i++)
                //        {
                //            totalReimburse += (int)Math.Ceiling(4.0 / 173 * (double)myPerson.Salary);
                //        }
                //    }
                //}
                else
                {
                    return StatusCode(403, new { status = HttpStatusCode.Forbidden, message = "Error : Invalid input DayType..." });
                }
                var reqOvertime = new Overtime
                {

                    NIK = NIK,
                    Date = DateTime.Now,
                    StartTime = reqOvertimeVM.StartTime,
                    EndTime = reqOvertimeVM.EndTime,
                    DescEmp = reqOvertimeVM.DescEmp,
                    DescHead = reqOvertimeVM.DescHead,
                    TotalReimburse = totalReimburse,
                    DayTypeId = reqOvertimeVM.DayTypeId,
                    StatusId = reqOvertimeVM.StatusId
                    
                };
                if (reqOvertime.TotalReimburse < 0)
                {
                    return StatusCode(403   , new { status = HttpStatusCode.Forbidden, message = "Error : Invalid StartTime and EndTime Invalid..." });
                }
                else
                {
                    overtimeContext.Overtime.Add(reqOvertime);
                    var addReq = overtimeContext.SaveChanges();

                   // var user = myPerson. Where(u => u.NIK == reqOvertimeVM.NIK).FirstOrDefault();
                    var subject = "Request for An Asset";
                    var body = $"Request lembur baru untuk divalidasi telah diterima! Request validasi telah dikirim atas nama: {myPerson.FirstName} nik: {myPerson.NIK},\nMohon untuk ditinjau kembali sebelum melakukan validasi.\n Terima kasih dan Selamat Bekerja.";
                    sendMail.SendEmail(myHeadPerson.Email, body, subject);
                    return StatusCode(200, new { status = HttpStatusCode.OK, message = "Requested for validation" });
                   
                }
            }
            return NotFound();
        }
        [HttpPut("validating")]
        public ActionResult Validating(Overtime overtime)
        {
            try
            {

                overtimeContext.Overtime.Update(overtime);
                var myPerson = overtimeContext.Person.FirstOrDefault(i => i.NIK == overtime.NIK);
                var validate = overtimeContext.SaveChanges();
                var status = overtimeContext.Status.FirstOrDefault(i => i.Id == overtime.StatusId);
                var myHead = overtimeContext.RoleAccount.FirstOrDefault(i => i.RoleId == 2);
                //var myHeadAcc = overtimeContext.Account.FirstOrDefault(u => u.NIK == myHead.NIK);
                var myHeadPerson = overtimeContext.Person.FirstOrDefault(u => u.NIK == myHead.NIK);

                var subject = $"{status.Name} request lembur";
                var body = $"Hello {myPerson.FirstName}!\n Request validasi lembur anda pada tanggal {overtime.Date} telah di tinjau oleh Head Department!\n Deskripsi lembur: {overtime.DescEmp}\n Deskripsi validasi: {overtime.DescHead} \n Validation Status : {status.Name} \n Untuk keterangan lebih lanjut silahkan hubungi Head Department pada email: {myHeadPerson.Email}\n Terima kasih dan Selamat Bekerja.";
                sendMail.SendEmail(myPerson.Email, body, subject);
                //return StatusCode(200, new { status = HttpStatusCode.OK, message = "Requested for validation" });

                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Validation success" });
            }
            catch (Exception)
            {
                return StatusCode(400, new { status = HttpStatusCode.NotModified, message = "Error : Data not updated" });
            }
        }

    }
}
