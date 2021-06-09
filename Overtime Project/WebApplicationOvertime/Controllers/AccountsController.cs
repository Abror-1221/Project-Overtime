using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Overtime_Project.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplicationOvertime.Base;
using WebApplicationOvertime.Repository.Data;

namespace WebApplicationOvertime.Controllers
{
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository repository;

        public AccountsController(AccountRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetUserData()
        {
            var result = await repository.GetUserData();
            return Json(result);
        }

        public IActionResult Head()
        {
            return View("/Views/TestCORS/Head.cshtml");
        }

        public IActionResult HeadHistory()
        {
            return View("/Views/TestCORS/HeadHistory.cshtml");
        }

        public IActionResult Employee()
        {
        //    var token = HttpContext.Session.GetString("token");
        //    ViewData["token"] = token;

        //    var jwtReader = new JwtSecurityTokenHandler();
        //    var jwt = jwtReader.ReadJwtToken(token);
        //    var nik = jwt.Claims.First(c => c.Type == "NIK").Value;
        //    ViewData["nik"] = nik;
            return View("/Views/TestCORS/Employee.cshtml");
        }
        public IActionResult EmployeeHistory()
        {
            //    var token = HttpContext.Session.GetString("token");
            //    ViewData["token"] = token;

            //    var jwtReader = new JwtSecurityTokenHandler();
            //    var jwt = jwtReader.ReadJwtToken(token);
            //    var nik = jwt.Claims.First(c => c.Type == "NIK").Value;
            //    ViewData["nik"] = nik;
            return View("/Views/TestCORS/EmployeeHistory.cshtml");
        }

        [HttpGet("Accounts/GetUserOvertime/{nik}")]
        public async Task<JsonResult> GetUserOvertime(string nik)
        {
            var result = await repository.GetUserOvertime(nik);
            return Json(result);
        } 
        [HttpGet("Accounts/GetUserOvertimeHisotry/{nik}")]
        public async Task<JsonResult> GetUserOvertimeHistory(string nik)
        {
            var result = await repository.GetUserOvertimeHistory(nik);
            return Json(result);
        }
        //[HttpGet]
        //public String Get()
        //{
        //    var token = HttpContext.Session.GetString("token");
        //    ViewData["token"] = token;

        //    var jwtReader = new JwtSecurityTokenHandler();
        //    var jwt = jwtReader.ReadJwtToken(token);
        //    var nik = jwt.Claims.First(c => c.Type == "NIK").Value;

        //    var httpClient = new HttpClient();
        //    var response = httpClient.GetAsync("https://localhost:44324/api/overtime/OvertimeData/"+nik).Result;
        //    var apiResponse = response.Content.ReadAsStringAsync();
        //    return apiResponse.Result;
        //}
    }
}
