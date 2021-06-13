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
            var daytype = 0;
            var hourOvertime = 0;
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
                hourOvertime = (int)Math.Ceiling(Convert.ToDouble((reqOvertimeVM.EndTime.TimeOfDay - reqOvertimeVM.StartTime.TimeOfDay).TotalHours));
                hour = (int)Math.Ceiling(Convert.ToDouble((reqOvertimeVM.EndTime - reqOvertimeVM.StartTime).TotalHours));
                if ((reqOvertimeVM.StartTime.DayOfWeek == DayOfWeek.Sunday || reqOvertimeVM.StartTime.DayOfWeek == DayOfWeek.Saturday) && myPerson.OvertimeHour >= hourOvertime)
                {
                    
                    if (hourOvertime <= 8)
                    {
                        totalReimburse += (int)Math.Ceiling((double)hourOvertime * 2 / 173 * myPerson.Salary);
                    }
                    else
                    {
                        totalReimburse += (int)Math.Ceiling(8.0 * 2 / 173 * (double)myPerson.Salary);
                        totalReimburse += (int)Math.Ceiling(3.0 / 173 * (double)myPerson.Salary);
                        for (int i=2 ; i <= hourOvertime - 8 ; i++)
                        {
                            totalReimburse += (int)Math.Ceiling(4.0 / 173 * (double)myPerson.Salary);
                        }
                    }
                    daytype = 1;
                }
                else if ((reqOvertimeVM.StartTime.DayOfWeek != DayOfWeek.Sunday || reqOvertimeVM.StartTime.DayOfWeek != DayOfWeek.Saturday) && myPerson.OvertimeHour >= hourOvertime)
                {
                    totalReimburse = (int)Math.Ceiling(1.5 / 173 * myPerson.Salary);
                    for (int i = 1; i <= hour - 1; i++)
                    {
                        totalReimburse += (int)Math.Ceiling(2.0 / 173 * myPerson.Salary); ;
                    }
                    daytype = 2;
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
                    DayTypeId = daytype,
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
                    string isBody = String.Empty;

                   
                    // var user = myPerson. Where(u => u.NIK == reqOvertimeVM.NIK).FirstOrDefault();
                    var subject = $"Request validation overtime [{reqOvertime.Date.ToString("dd-MM-yyyy")}]";
                    //var body = $"Request lembur baru untuk divalidasi telah diterima! Request validasi telah dikirim atas nama: {myPerson.FirstName} nik: {myPerson.NIK},\nMohon untuk ditinjau kembali sebelum melakukan validasi.\n Terima kasih dan Selamat Bekerja.";
                    isBody = "<!DOCTYPE html>";
                    isBody += "<html lang='en' xmlns='http://www.w3.org/1999/xhtml' xmlns:o='urn:schemas-microsoft-com:office:office'>";
                    isBody += "<head>";
                    isBody += "<style>";
                    isBody += "table, td, div, h1, p {";
                    isBody += "font-family: Arial, sans-serif;";
                    isBody += "}";
                    isBody += "</style>";
                    isBody += "</head>";
                    isBody += "<body style='margin:0; padding:0;'>";
                    isBody += "<body style='margin:0; padding:0;'>";
                    isBody += "<table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;background:#ffffff;'>";
                    isBody += "<tr> <td align='center' style='padding:0;'>";
                    isBody += "<table role='presentation' style='width:602px;border-collapse:collapse;border:1px solid #cccccc;border-spacing:0;text-align:left;'>";
                    isBody += "<tr>";
                    isBody += "<td align='center' style='padding:40px 0 30px 0;background:#ffffff;'>";
                    isBody += "<img src='https://www.synnexmetrodata.com/wp-content/uploads/2020/06/Logo-SMI-300-x-80-pixel.png' alt='' width='300' style='height:auto;display:block;' />";
                    isBody += "</td>";
                    isBody += "</tr>";
                    isBody += "<tr>";
                    isBody += "<td style='padding:36px 30px 42px 30px;'>";
                    isBody += "<table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;'>";
                    isBody += "<tr>";
                    isBody += "<td style='padding:0 0 36px 0;color:#153643;'>";
                    isBody += $"<h1 style='font-size:24px;margin:0 0 20px 0;font-family:Arial,sans-serif;'>Overtime Validation Requested</h1>";
                    isBody += $"<p style='margin:0 0 12px 0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'>Halo <b>{myHeadPerson.FirstName}</b></p>";
                    isBody += $"<p>Request lembur baru untuk divalidasi telah diterima!</p>";
                    isBody += $"<p>Terdata request validasi lembur: </p>";
                    isBody += $"<p>Tanggal request validasi: <b>{reqOvertime.Date.ToString("dd-MM-yyyy")}</b></p>";
                    isBody += $"<p>Atas nama:<b>{myPerson.FirstName} {myPerson.LastName}</b> </p>";
                    isBody += $"<p>Deskripsi lembur: <b>{reqOvertime.DescEmp}</b></p>";
                    isBody += $"<p>Mohon untuk ditinjau kembali sebelum melakukan validasi!</p>";
                    isBody += $"<p>Terima kasih!</p>";
                    isBody += $"</td>";
                    isBody += $"</tr>";
                    isBody += $"</table>";
                    isBody += $"</td>";
                    isBody += $"</tr>";
                    isBody += $"<tr>";
                    isBody += $"<td style='padding:30px;background:#ee4c50;'>";
                    isBody += $"<table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;font-size:9px;font-family:Arial,sans-serif;'>";
                    isBody += $"<tr>";
                    isBody += $"<td style='padding:0;width:50%;' align='left'>";
                    isBody += $"<p style='margin:0;font-size:14px;line-height:16px;font-family:Arial,sans-serif;color:#ffffff;'>";
                    isBody += "&reg; PT Synnex Metrodata Indonesia 2021<br /><a href='https://www.mii.co.id/' style='color:#ffffff;text-decoration:underline;'>Website</a>";
                    isBody += $"</p>";
                    isBody += $"</td>";
                    isBody += $"</tr>";
                    isBody += $"</table>";
                    isBody += $"</td>";
                    isBody += $"</tr>";
                    isBody += $"</table>"; 
                    isBody += $"</td>";
                    isBody += $"</tr>";
                    isBody += $"</table>";
                    isBody += $"</body>";
                    isBody += $"</html>";

                    ////////////////
                   

                    var body = isBody;


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

                var subject = $"{status.Name} request lembur [{overtime.Date.ToString("dd-MM-yyyy")}]";
                string isBody = String.Empty;
                isBody = "<!DOCTYPE html>";
                isBody += "<html lang='en' xmlns='http://www.w3.org/1999/xhtml' xmlns:o='urn:schemas-microsoft-com:office:office'>";
                isBody += "<head>";
                isBody += "<style>";
                isBody += "table, td, div, h1, p {";
                isBody += "font-family: Arial, sans-serif;";
                isBody += "}";
                isBody += "</style>";
                isBody += "</head>";
                isBody += "<body style='margin:0; padding:0;'>";
                isBody += "<body style='margin:0; padding:0;'>";
                isBody += "<table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;background:#ffffff;'>";
                isBody += "<tr> <td align='center' style='padding:0;'>";
                isBody += "<table role='presentation' style='width:602px;border-collapse:collapse;border:1px solid #cccccc;border-spacing:0;text-align:left;'>";
                isBody += "<tr>";
                isBody += "<td align='center' style='padding:40px 0 30px 0;background:#ffffff;'>";
                isBody += "<img src='https://www.synnexmetrodata.com/wp-content/uploads/2020/06/Logo-SMI-300-x-80-pixel.png' alt='' width='300' style='height:auto;display:block;' />";
                isBody += "</td>";
                isBody += "</tr>";
                isBody += "<tr>";
                isBody += "<td style='padding:36px 30px 42px 30px;'>";
                isBody += "<table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;'>";
                isBody += "<tr>";
                isBody += "<td style='padding:0 0 36px 0;color:#153643;'>";

                isBody += $"<h1 style='font-size:24px;margin:0 0 20px 0;font-family:Arial,sans-serif;'>Overtime Validation {status.Name}</h1>";
                isBody += $"<p style='margin:0 0 12px 0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'>Hello <b>{myPerson.FirstName}</b>!</b></p>";
                isBody += $"<p>Request validasi lembur anda pada tanggal {overtime.Date.ToString("dd-MM-yyyy")} telah di tinjau oleh Head Department!</p>";
                isBody += $"<p>Deskripsi lembur: {overtime.DescEmp}</p>";
                isBody += $"<p>Deskripsi validasi: {overtime.DescHead}</p>";
                isBody += $"<p>Validation status : {status.Name}</p>";
                isBody += $"<p>Untuk keterangan lebih lanjut silahkan hubungi Head Department pada email: {myHeadPerson.Email}</p>";
                isBody += "<p>Terima kasih dan Selamat Bekerja</p>";
                
                isBody += $"</td>";
                isBody += $"</tr>";
                isBody += $"</table>";
                isBody += $"</td>";
                isBody += $"</tr>";
                isBody += $"<tr>";
                isBody += $"<td style='padding:30px;background:#ee4c50;'>";
                isBody += $"<table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;font-size:9px;font-family:Arial,sans-serif;'>";
                isBody += $"<tr>";
                isBody += $"<td style='padding:0;width:50%;' align='left'>";
                isBody += $"<p style='margin:0;font-size:14px;line-height:16px;font-family:Arial,sans-serif;color:#ffffff;'>";
                isBody += "&reg; PT Synnex Metrodata Indonesia 2021<br /><a href='https://www.mii.co.id/' style='color:#ffffff;text-decoration:underline;'>Website</a>";
                isBody += $"</p>";
                isBody += $"</td>";
                isBody += $"</tr>";
                isBody += $"</table>";
                isBody += $"</td>";
                isBody += $"</tr>";
                isBody += $"</table>";
                isBody += $"</td>";
                isBody += $"</tr>";
                isBody += $"</table>";
                isBody += $"</body>";
                isBody += $"</html>";
                ////////////////////////////
               
                var body = isBody;
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
