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
        public IActionResult AdminProfile()
        {
            var token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                //return View("/Views/Authenticate/login.cshtml");
                return View("/Views/Authenticate/NewLogin2.cshtml");
            }
            ViewData["token"] = token;

            var jwtReader = new JwtSecurityTokenHandler();
            var jwt = jwtReader.ReadJwtToken(token);
            var nik = jwt.Claims.First(c => c.Type == "NIK").Value;
            var fname = jwt.Claims.First(c => c.Type == "First Name").Value;
            ViewData["fname"] = fname;
            ViewData["nik"] = nik;
            return View("/Views/TestCORS/AdminProfile.cshtml");
        }
        public IActionResult HeadProfile()
        {
            var token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                //return View("/Views/Authenticate/login.cshtml");
                return View("/Views/Authenticate/NewLogin2.cshtml");
            }
            ViewData["token"] = token;

            var jwtReader = new JwtSecurityTokenHandler();
            var jwt = jwtReader.ReadJwtToken(token);
            var nik = jwt.Claims.First(c => c.Type == "NIK").Value;
            var fname = jwt.Claims.First(c => c.Type == "First Name").Value;
            ViewData["fname"] = fname;
            ViewData["nik"] = nik;
            return View("/Views/TestCORS/HeadProfile.cshtml");
        }
        public IActionResult EmployeeProfile()
        {
            var token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                //return View("/Views/Authenticate/login.cshtml");
                return View("/Views/Authenticate/NewLogin2.cshtml");
            }
            ViewData["token"] = token;

            var jwtReader = new JwtSecurityTokenHandler();
            var jwt = jwtReader.ReadJwtToken(token);
            var nik = jwt.Claims.First(c => c.Type == "NIK").Value;
            var fname = jwt.Claims.First(c => c.Type == "First Name").Value;
            ViewData["fname"] = fname;
            ViewData["nik"] = nik;
            return View("/Views/TestCORS/EmployeeProfile.cshtml");
        }
        public IActionResult Head()
        {
            var token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                //return View("/Views/Authenticate/login.cshtml");
                return View("/Views/Authenticate/NewLogin2.cshtml");
            }
            ViewData["token"] = token;

            var jwtReader = new JwtSecurityTokenHandler();
            var jwt = jwtReader.ReadJwtToken(token);
            var nik = jwt.Claims.First(c => c.Type == "NIK").Value;
            var fname = jwt.Claims.First(c => c.Type == "First Name").Value;
            ViewData["fname"] = fname;
            ViewData["nik"] = nik;
            return View("/Views/TestCORS/Head.cshtml");
        }

        public IActionResult HeadHistory()
        {
            var token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                //return View("/Views/Authenticate/login.cshtml");
                return View("/Views/Authenticate/NewLogin2.cshtml");
            }
            ViewData["token"] = token;

            var jwtReader = new JwtSecurityTokenHandler();
            var jwt = jwtReader.ReadJwtToken(token);
            var nik = jwt.Claims.First(c => c.Type == "NIK").Value;
            var fname = jwt.Claims.First(c => c.Type == "First Name").Value;
            ViewData["fname"] = fname;
            ViewData["nik"] = nik;
            return View("/Views/TestCORS/HeadHistory.cshtml");
        }

        public IActionResult Employee()
        {
            var token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                //return View("/Views/Authenticate/login.cshtml");
                return View("/Views/Authenticate/NewLogin2.cshtml");
            }
            ViewData["token"] = token;

            var jwtReader = new JwtSecurityTokenHandler();
            var jwt = jwtReader.ReadJwtToken(token);
            var nik = jwt.Claims.First(c => c.Type == "NIK").Value;
            var fname = jwt.Claims.First(c => c.Type == "First Name").Value;
            ViewData["fname"] = fname;
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
            var token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                //return View("/Views/Authenticate/login.cshtml");
                return View("/Views/Authenticate/NewLogin2.cshtml");
            }
            ViewData["token"] = token;

            var jwtReader = new JwtSecurityTokenHandler();
            var jwt = jwtReader.ReadJwtToken(token);
            var nik = jwt.Claims.First(c => c.Type == "NIK").Value;
            var fname = jwt.Claims.First(c => c.Type == "First Name").Value;
            ViewData["fname"] = fname;
            ViewData["nik"] = nik;
            return View("/Views/TestCORS/EmployeeHistory.cshtml");
        }

        //[HttpGet("Accounts/GetUserOvertime/{nik}")]
        [HttpGet("Accounts/GetUserOvertime/")]
        public async Task<JsonResult> GetUserOvertime()
        {
            var token = HttpContext.Session.GetString("token");
            ViewData["token"] = token;

            var jwtReader = new JwtSecurityTokenHandler();
            var jwt = jwtReader.ReadJwtToken(token);
            var id = jwt.Claims.First(c => c.Type == "NIK").Value;

            var result = await repository.GetUserOvertime(id);
            return Json(result);
        } 
        //[HttpGet("Accounts/GetUserOvertimeHisotry/{nik}")]
        [HttpGet("Accounts/GetUserOvertimeHistory/")]
        public async Task<JsonResult> GetUserOvertimeHistory()
        {
            var token = HttpContext.Session.GetString("token");
            ViewData["token"] = token;

            var jwtReader = new JwtSecurityTokenHandler();
            var jwt = jwtReader.ReadJwtToken(token);
            var id = jwt.Claims.First(c => c.Type == "NIK").Value;

            var result = await repository.GetUserOvertimeHistory(id);
            return Json(result);
        }

        [HttpGet("Accounts/GetNIK/")]
        public string GetNIK()

        {
            var token = HttpContext.Session.GetString("token");
            ViewData["token"] = token;

            var jwtReader = new JwtSecurityTokenHandler();
            var jwt = jwtReader.ReadJwtToken(token);
            var id = jwt.Claims.First(c => c.Type == "NIK").Value;
            return id;
        }

        //[HttpPost("Accounts/ReqOvertime/")]
        //public async Task<JsonResult> ReqOvertime()
        //{
        //    var token = HttpContext.Session.GetString("token");
        //    ViewData["token"] = token;

        //    var jwtReader = new JwtSecurityTokenHandler();
        //    var jwt = jwtReader.ReadJwtToken(token);
        //    var id = jwt.Claims.First(c => c.Type == "NIK").Value;

        //    var result = await repository.ReqOvertime(id, ReqOvertimeVM entity);
        //    return Json(result);
        //}
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
