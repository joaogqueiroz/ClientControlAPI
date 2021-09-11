using AspNetAPIProject01.Data.Interfaces;
using AspNetAPIProject01.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetAPIProject01.Services
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

            services.AddControllers();

            //Getting connectionstring
            var connectionstring = Configuration.GetConnectionString("Context_DB");

            //Dependency injection
            services.AddTransient<IClientRepository, ClientRepository>(map => new ClientRepository(connectionstring));

            //Configure API documentation(swagger)
            services.AddSwaggerGen(
                swagger => 
                {
                    swagger.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "Client control API.",
                        Description = "Project using AspNet 5 API, SQL Server and Dapper.",
                        Version ="v1",
                        Contact = new OpenApiContact 
                        {
                            Name = "Client Control",
                            //Url = new Uri(""),
                            //Email = ""
                        }
                    }) ;
                }
                );

            //Configure CORS
            services.AddCors(
                s => s.AddPolicy("DefaultPolicy", builder => 
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();

                    })
                );

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //Configure api documentation(swagger) including initial config
            app.UseSwagger();
            app.UseSwaggerUI( s=> { s.SwaggerEndpoint("/swagger/v1/swagger.json", "Client Control"); });

            app.UseRouting();

            app.UseCors("DefaultPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
