using Microsoft.AspNetCore.Mvc;
using Overtime_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet("Accounts/GetUserOvertime/{nik}")]
        public async Task<JsonResult> GetUserOvertime(string nik)
        {
            var result = await repository.GetUserOvertime(nik);
            return Json(result);
        }
    }
}
