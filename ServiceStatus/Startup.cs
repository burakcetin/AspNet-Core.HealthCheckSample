using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

//using  Microsoft.Extensions.Diagnostics.HealthCheck;
namespace ServiceStatus
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
           
           services
               .AddHealthChecksUI()
                   
               .AddHealthChecks()
               
               .Services
               .AddMvc()
               .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHealthChecks("/healthz", new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                })
                .UseHealthChecksUI(setup =>
                {
                    setup.UIPath = "/monitoring-web"; 
                    setup.ApiPath = "/monitoring-api";  
                }).UseMvc();
        }
    }
}
