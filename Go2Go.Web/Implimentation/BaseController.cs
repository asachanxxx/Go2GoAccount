using Go2Go.Implementations;
using Go2Go.Model.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Go2Go.Web.Implimentation
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController<TEntity, TRepository> : ControllerBase
         where TEntity : class, IEntity
         where TRepository : IRepository<TEntity>
    {
        private readonly TRepository repository;

        public BaseController(TRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TEntity>>> Get()
        {
            var resultsx = await repository.GetAll();
            return resultsx;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TEntity>> Get(int id)
        {
            try
            {
                var entity = await repository.Get(id);
                if (entity == null)
                {
                    return NotFound();
                }
                return entity;
            }
            catch (Exception ex)
            {
                return ex.InnerException != null ? StatusCode(500, ex.InnerException?.Message) : StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TEntity entity)
        {
            try
            {
                if (id != entity.Id)
                {
                    return BadRequest();
                }
                await repository.Update(entity);
                return NoContent();
            }
            catch (Exception ex)
            {
                return ex.InnerException != null ? StatusCode(500, ex.InnerException?.Message) : StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<TEntity>> Post(TEntity entity)
        {
            try
            {
                await repository.Add(entity);
                return CreatedAtAction("Get", new { id = entity.Id }, entity);
            }
            catch (Exception ex)
            {
                return ex.InnerException != null ? StatusCode(500, ex.InnerException?.Message) : StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TEntity>> Delete(int id)
        {
            try
            {
                var entity = await repository.Delete(id);
                if (entity == null)
                {
                    return NotFound();
                }
                return entity;
            }
            catch (Exception ex)
            {
                return ex.InnerException != null ? StatusCode(500, ex.InnerException?.Message) : StatusCode(500, ex.Message);
            }
        }

    }
}
