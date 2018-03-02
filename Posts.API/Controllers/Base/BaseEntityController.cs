using System.Collections.Generic;
using System.Threading.Tasks;
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

        protected async Task<ActionResult> GetActionAsync()
        {
            var entities = await Service.GetList();

            var models = Mapper.Map<IList<TModel>>(entities);

            return Ok(models);
        }

        protected async Task<ActionResult> GetActionAsync(int id)
        {
            var entity = await Service.Get(id);

            var model = Mapper.Map<TModel>(entity);

            return Ok(model);
        }

        protected async Task<ActionResult> PostActionAsync(TModel model)
        {
            var entity = Mapper.Map<TEntity>(model);

            await Service.Create(entity);

            return Ok(new {success = true});
        }

        protected async Task<ActionResult> PutActionAsync(int id, TModel model)
        {
            var entity = Mapper.Map<TEntity>(model);
            entity.Id = id;

            await Service.Update(entity);

            return Ok(new { success = true });
        }

        protected async Task<ActionResult> DeleteActionAsync(int id)
        {
            await Service.Delete(id);

            return Ok(new { success = true });
        }
    }
}
