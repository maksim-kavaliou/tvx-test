using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Posts.DomainEntities.Entities.Base;
using Posts.Services.Interfaces.Base;
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

        protected ActionResult GetAction()
        {
            var entities = Service.GetList();

            var models = Mapper.Map<IList<TModel>>(entities);

            return Json(models);
        }

        protected ActionResult GetAction(int id)
        {
            var entity = Service.Get(id);

            var model = Mapper.Map<TModel>(entity);

            return Json(model);
        }

        protected ActionResult PostAction(TModel model)
        {
            var entity = Mapper.Map<TEntity>(model);

            Service.Create(entity);

            return Json(new {success = true});
        }

        protected ActionResult PutAction(int id, TModel model)
        {
            var entity = Mapper.Map<TEntity>(model);
            entity.Id = id;

            Service.Update(entity);

            return Json(new { success = true });
        }

        protected ActionResult DeleteAction(int id)
        {
            Service.Delete(id);

            return Json(new { success = true });
        }
    }
}
