﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationOvertime.Controllers
{
    public class TestCORS : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
