using Catan.API.DependencyInjection;
using Catan.API.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace Catan.API
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
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(opts =>
                    opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());

            services.AddSignalR();
            services.AddCors();
            services.AddCatanCore();
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

            app.UseCors(builder => builder
                //.AllowAnyOrigin() //works in .net core 2.1 but not in 2.2
                .WithOrigins("http://localhost:8080") //from .net core 2.2 onwards
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
            );

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSignalR(route =>
            {
                route.MapHub<ClientHub>("/hubs/client");
            });
        }
    }
}
