using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApplicationOvertime.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public String Get()
        {
            var token = HttpContext.Session.GetString("token");
            ViewData["token"] = token;

            var jwtReader = new JwtSecurityTokenHandler();
            var jwt = jwtReader.ReadJwtToken(token);
            var id = jwt.Claims.First(c => c.Type == "NIK").Value;

            var httpClient = new HttpClient();
            var response = httpClient.GetAsync("https://localhost:44324/API/Account/userdata/" + id).Result;
            var apiResponse = response.Content.ReadAsStringAsync();
            return apiResponse.Result;
        }
        [HttpGet("RequestOvertime")]
        public String GetOvertimeNik()
        {
            var token = HttpContext.Session.GetString("token");
            ViewData["token"] = token;

            var jwtReader = new JwtSecurityTokenHandler();
            var jwt = jwtReader.ReadJwtToken(token);
            var id = jwt.Claims.First(c => c.Type == "NIK").Value;

            var httpClient = new HttpClient();
            var response = httpClient.GetAsync("https://localhost:44324/API/Account/userdata/" + id).Result;
            var apiResponse = response.Content.ReadAsStringAsync();
            return apiResponse.Result;
        }
    }
}
