using System;
using System.IO;
using GCBattleShip.Domain.Interfaces.Services;
using GCBattleShip.Domain.Services;
using GCBattleShip.Infrastructure.MongoDb;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GCBattleShip.Api
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
            services.AddMvc();
            services.AddSingleton<IGameService, GameService>();
            services.Configure<MongoDbSettings>(Configuration.GetSection("Mongodb"));
            services.RegisterMongodbServices();
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.DescribeStringEnumsInCamelCase();
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "GCBattleShip API",
                    Version = "v1",
                    Description = "The API for the GC Battleship tracker"
                });
                var filePath = Path.Combine(AppContext.BaseDirectory, "GCBattleShipApi.xml");
                options.IncludeXmlComments(filePath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger(c=>
                    c.RouteTemplate="api/swagger/{documentName}/swagger.json")
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/api/swagger/v1/swagger.json", "HTTP API V1");
                    c.RoutePrefix = "api/docs";
                });
        }
    }
}