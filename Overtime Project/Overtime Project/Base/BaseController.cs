using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Overtime_Project.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Overtime_Project.Base
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]

    public class BaseController<Entity, Repository, Key> : ControllerBase
        where Entity : class
        where Repository : IRepository<Entity, Key>
    {
        private readonly Repository repo;
        public BaseController(Repository repository)
        {
            this.repo = repository;
        }

        [HttpPost]
        public ActionResult Post(Entity entity)
        {
            try
            {
                repo.Insert(entity);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(400, new { status = HttpStatusCode.Forbidden, message = $"Error : Column name doesn't match or PK column is null!!" });
            }
        }

        [HttpGet]
        public ActionResult Get()
        {
            IEnumerable<Entity> entities = repo.Get();
            return Ok(entities);
        }

        [HttpGet("{Key}")]
        public ActionResult Get(Key key)
        {
            var entity = repo.Get(key);
            if (entity != null)
            {
                return Ok(entity);
            }
            else
            {
                return NotFound($"Error : {key} not found!!");
            }
        }

        [HttpDelete("{Key}")]
        public ActionResult Delete(Key key)
        {
            var entity = repo.Get(key);
            if (entity != null)
            {
                return Ok(repo.Delete(key));
            }
            else
            {
                return NotFound($"Error : The {key} that wanted to delete was not found!!");
            }
        }

        [HttpPut]
        public ActionResult Update(Entity entity)
        {
            try
            {
                repo.Update(entity);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(400, new { status = HttpStatusCode.NotModified, message = "Error : Data not updated" });
            }
        }
    }
}
