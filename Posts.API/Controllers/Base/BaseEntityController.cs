using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Posts.DomainEntities.Entities.Base;
using Posts.Services.Interfaces.Base;
using Posts.Web.Core.Models.Base;

namespace Posts.API.Controllers.Base
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public abstract class BaseEntityController<TEntity, TModel> : Controller
        where TEntity : DomainEntity
        where TModel : BaseModel
    {
        protected readonly IMapper Mapper;
        protected readonly IEntityService<TEntity> Service;

        protected BaseEntityController(IMapper mapper, IEntityService<TEntity> service)
        {
            Mapper = mapper;
            Service = service;
        }

        // GET: api/BaseEntity
        [HttpGet]
        public IEnumerable<string> Get()
        {
            

            return new string[] { "value1", "value2" };
        }

        // GET: api/BaseEntity/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult Get(int id)
        {
            var entity = Service.Get(id);

            var model = Mapper.Map<TModel>(entity);

            return Json(model);
        }
        
        // POST: api/BaseEntity
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/BaseEntity/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
