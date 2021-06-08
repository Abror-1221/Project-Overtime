using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Overtime_Project.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationOvertime.Controllers
{
    public class AuthenticateController : Controller
    {
        [Route("login")]
        public IActionResult Login()
        {
            return View();//"~/Views/Auth/Login.cshtml"
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public string Login(LogInVM loginVM)
        {
            var httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(loginVM), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync("https://localhost:44324/api/Account/Login", content).Result;


            var token = result.Content.ReadAsStringAsync().Result;
            HttpContext.Session.SetString("token", token);

            if (result.IsSuccessStatusCode)
            {
                var jwtReader = new JwtSecurityTokenHandler();
                var jwt = jwtReader.ReadJwtToken(token);

                var role = jwt.Claims.First(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value;
                if (role == "Head")
                {
                    return Url.Action("Head", "TestCORS");
                }
                else if (role == "Admin")
                {
                    //return Url.Action("Index", "TestCORS");
                    return Url.Action("","TestCORS");
                }
                else if (role == "Employee")
                {
                    return Url.Action("Employee", "TestCORS");
                }
              
                else
                {
                    return Url.Action("Index", "Home");
                }
            }
            else
            {
                return Url.Action("Error", "Home");
                //return BadRequest(new { result });
            }
            }
    }
}
