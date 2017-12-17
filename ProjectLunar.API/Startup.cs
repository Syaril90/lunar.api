using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ProjectLunar.API.Models;
using Swashbuckle.AspNetCore.Swagger;
using ProjectLunar.API.Services;
using ProjectLunar.API.Interfaces;
using ProjectLunar.API.Repositories;
using ProjectLunar.API.Filters;

namespace ProjectLunar.API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<LunarContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            // Register entity.
            services.AddScoped<IEntityRepository<PrayerPlace>, EntityRepository<PrayerPlace>>();
            services.AddScoped<IEntityRepository<Photo>, EntityRepository<Photo>>();
            services.AddScoped<IEntityRepository<User>, EntityRepository<User>>();
            services.AddScoped<IEntityRepository<UserAction>, EntityRepository<UserAction>>();
            // Register application services.
            services.AddScoped<IPrayerPlaceService, PrayerPlaceService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserActionService, UserActionService>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            //app.UseMiddleware<AuthorizeAttribute>();
            app.UseMvc();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            
        }
    }
}
