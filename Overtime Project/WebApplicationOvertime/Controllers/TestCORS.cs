using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationOvertime.Controllers
{
    public class TestCORS : Controller
    {
        public IActionResult Index()
        {
            var token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                //return View("/Views/Authenticate/login.cshtml");
                return View("/Views/Authenticate/NewLogin2.cshtml");
                //return View("/Views/Authenticate/NewLogin.cshtml");
            }
            ViewData["token"] = token;

            var jwtReader = new JwtSecurityTokenHandler();
            var jwt = jwtReader.ReadJwtToken(token);
            var nik = jwt.Claims.First(c => c.Type == "NIK").Value;
            var fname = jwt.Claims.First(c => c.Type == "First Name").Value;
            ViewData["fname"] = fname;
            ViewData["nik"] = nik;
            return View();
        } public IActionResult Employee()
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
            return View();
        } public IActionResult Head()
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
            return View();
        }
        public IActionResult EmployeeHistory()
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
            return View();
        }
        public IActionResult EmailTemplate()
        {
           
            return View("/Views/testcors/Emailtemplate.cshtml");
        }
    }
}
