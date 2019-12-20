using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyCourse.Models.Services.Application;
using src.Models.Services.Application;

namespace CorsoDotNet
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()

            #if DEBUG
            .AddRazorRuntimeCompilation()
            #endif
            ;

            services.AddTransient<ICourseService, CourseService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           // if (env.IsDevelopment())

        //    app.UseHttpsRedirection();
           
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }

            app.UseStaticFiles();

            app.UseRouting();

            //app.UseMvcWithDefaultRoute();
            
            app.UseEndpoints(route => {
                route.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
