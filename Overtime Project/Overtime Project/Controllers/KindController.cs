using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Overtime_Project.Base;
using Overtime_Project.Models;
using Overtime_Project.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KindController : BaseController<Kind, KindRepository, int>
    {
        public KindController(KindRepository kindRepository) : base(kindRepository)
        {

        }
    }
}
