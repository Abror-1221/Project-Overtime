using Microsoft.AspNetCore.Authorization;
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
using System.Net;
using System.Threading.Tasks;

namespace Overtime_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OvertimeController : BaseController<Overtime, OvertimeRepository, int>
    {
        private readonly OvertimeRepository overtimeRepository;
        private readonly OvertimeContext overtimeContext;
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
                       where o.NIK == NIK
                       select new
                       {
                           Id = o.Id,
                           NIK = o.NIK,
                           Date = o.Date,
                           StartTime = o.StartTime,
                           EndTime = o.EndTime,
                           DescEmp = o.DescEmp,
                           TotalReimburse = o.TotalReimburse,
                           DayTypeId = o.DayTypeId,
                           TypeName = k.Name,
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
                       select new
                       {
                           Id = o.Id,
                           NIK = o.NIK,
                           Date = o.Date,
                           StartTime = o.StartTime,
                           EndTime = o.EndTime,
                           DescEmp = o.DescEmp,
                           TotalReimburse = o.TotalReimburse,
                           DayTypeId = o.DayTypeId,
                           DayTypeName = k.Name,
                           StatusId = o.StatusId,
                           StatusName = s.Name,
                       };
            return Ok(await data.ToListAsync());
        }
        
        [HttpPost("ReqOvertime/{NIK}")]
        [Authorize(Roles = "Employee")]
        public ActionResult RequestOvertime(string NIK,ReqOvertimeVM reqOvertimeVM)
        {
            var totalReimburse = 0;
            var hour = 0;
            var myPerson = overtimeContext.Person.FirstOrDefault(u => u.NIK == NIK);
            if (myPerson != null)
            {

                hour = (int)Math.Ceiling(Convert.ToDouble((reqOvertimeVM.EndTime - reqOvertimeVM.StartTime).TotalHours));
                if (reqOvertimeVM.DayTypeId == 1)
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
                else if (reqOvertimeVM.DayTypeId == 2)
                {
                    totalReimburse = (int)Math.Ceiling(1.5 / 173 * myPerson.Salary);
                    for (int i = 1; i <= hour - 1; i++)
                    {
                        totalReimburse += (int)Math.Ceiling(2.0 / 173 * myPerson.Salary); ;
                    }
                }
                else if (reqOvertimeVM.DayTypeId == 3)
                {
                    if (hour <= 5)
                    {
                        totalReimburse += (int)Math.Ceiling((double)hour * 2 / 173 * (double)myPerson.Salary);
                    }
                    else
                    {
                        totalReimburse += (int)Math.Ceiling(5.0 * 2 / 173 * (double)myPerson.Salary);
                        totalReimburse += (int)Math.Ceiling(3.0 / 173 * (double)myPerson.Salary);
                        for (int i = 2; i <= hour - 5; i++)
                        {
                            totalReimburse += (int)Math.Ceiling(4.0 / 173 * (double)myPerson.Salary);
                        }
                    }
                }
                else
                {
                    return StatusCode(403, new { status = HttpStatusCode.Forbidden, message = "Error : Invalid input DayType..." });
                }
                var reqOvertime = new Overtime
                {

                    NIK = NIK,
                    Date = reqOvertimeVM.Date,
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
                    return StatusCode(403, new { status = HttpStatusCode.Forbidden, message = "Error : Invalid StartTime and EndTime Invalid..." });
                }
                else
                {
                    overtimeContext.Overtime.Add(reqOvertime);
                    var addReq = overtimeContext.SaveChanges();
                    return Ok();
                }
            }
            return NotFound();
        }
    }
}
