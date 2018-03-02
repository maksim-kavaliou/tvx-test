using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Posts.API.Common.ActionFilters;
using Posts.DataAccess.Context;
using Posts.DataAccess.Interfaces;
using Posts.DataAccess.Repositories;
using Posts.Services.Interfaces;
using Posts.Services.Services;
using Posts.Web.Core.Mappers;
using Posts.Web.Core.Validators;

namespace Posts.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc(options => options.Filters.Add(typeof(ValidationActionFilter)))
                .AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<PostModelValidator>());

            ConfigureCustomServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }

        public virtual void ConfigureCustomServices(IServiceCollection services)
        {
            // automapper
            services.AddAutoMapper(typeof(MappingProfile));

            // EF context
            var connectionString = Configuration.GetConnectionString("DefaultConntection");
            services.AddDbContext<DefaultDbContext>(options => options.UseSqlServer(connectionString));

            // repositories
            services.AddScoped<IPostsRepository, PostsRepository>();
            services.AddScoped<ICommentsRepository, CommentsRepository>();

            // services
            services.AddScoped<IPostsService, PostsService>();
            services.AddScoped<ICommentsService, CommentsService>();
        }
    }
}
