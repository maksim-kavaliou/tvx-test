using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Posts.DomainEntities.Entities.Base;
using Posts.Services.Interfaces.Base;
using Posts.Web.Core.Models;
using Posts.Web.Core.Models.Base;

namespace Posts.API.Controllers.Base
{
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
        public ActionResult Get()
        {
            var entities = Service.GetList();

            var models = Mapper.Map<IList<TModel>>(entities);

            return Json(models);
        }

        // GET: api/BaseEntity/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var entity = Service.Get(id);

            var model = Mapper.Map<TModel>(entity);

            return Json(model);
        }
        
        // POST: api/BaseEntity
        [HttpPost]
        public ActionResult Post([FromBody]TModel model)
        {
            var entity = Mapper.Map<TEntity>(model);

            Service.Create(entity);

            return Json(new {success = true});
        }
        
        // PUT: api/BaseEntity/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]TModel model)
        {
            var entity = Mapper.Map<TEntity>(model);

            Service.Update(entity);

            return Json(new { success = true });
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Service.Delete(id);

            return Json(new { success = true });
        }
    }
}
