using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public ActionResult ViewData(string NIK)
        {
            var data = from p in overtimeContext.Person
                       join a in overtimeContext.Account on p.NIK equals a.NIK
                       join ar in overtimeContext.RoleAccount on a.NIK equals ar.NIK
                       join r in overtimeContext.Role on ar.RoleId equals r.Id
                       join o in overtimeContext.Overtime on p.NIK equals o.NIK
                       join s in overtimeContext.Status on o.StatusId equals s.Id
                       join k in overtimeContext.Kind on o.KindId equals k.Id
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
                           TypeId = o.KindId,
                           TypeName = k.Name,
                           StatusId = o.StatusId,
                           StatusName = s.Name,
                       };
            return Ok(data.ToList());
        }

        [HttpPost("ReqOvertime/{NIK}")]
        //[Authorize(Roles = "Admin")]
        public ActionResult RequestOvertime(string NIK,ReqOvertimeVM reqOvertimeVM)
        {
            var myPerson = overtimeContext.Person.FirstOrDefault(u => u.NIK == NIK);
            if (myPerson != null)
            {

                var reqOvertime = new Overtime
                {
                    NIK = NIK,
                    Date = reqOvertimeVM.Date,
                    StartTime = reqOvertimeVM.StartTime,
                    EndTime = reqOvertimeVM.EndTime,
                    DescEmp = reqOvertimeVM.DescEmp,
                    DescHead = reqOvertimeVM.DescHead,
                    TotalReimburse = ((reqOvertimeVM.EndTime - reqOvertimeVM.StartTime)*myPerson.Salary),
                    KindId = reqOvertimeVM.KindId,
                    StatusId = reqOvertimeVM.StatusId

                };
                overtimeContext.Overtime.Add(reqOvertime);
                var addReq = overtimeContext.SaveChanges();

                return Ok();
            }
            return NotFound();
        }
    }
}
