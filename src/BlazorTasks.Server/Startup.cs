using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AutoMapper;
using BlazorTasks.Server.Data;
using BlazorTasks.Server.Filters;
using BlazorTasks.Server.Models;
using BlazorTasks.Server.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BlazorTasks.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            #region Options
            services.AddOptions();
            services.Configure<DatabaseOptions>(Configuration.GetSection("DatabaseOptions"));
            var settings = Configuration.GetSection("DatabaseOptions").Get<DatabaseOptions>();
            #endregion

            services.AddDbContextPool<DatabaseContext>(opt => 
                {
                    if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && settings.UseInMemoryDatabase)
                    {
                        opt.UseInMemoryDatabase("blazor_tasks");
                    }
                    else opt.UseMySql(settings.ConnectionString);
                });

            services.AddAutoMapper();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddMvc(opt =>
            {
                opt.Filters.Add(typeof(JsonExceptionFilter));
                
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors("AllowAll");

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
