using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CorsoDotNet.Models.Services.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyCourse.Models.Services.Application;
using MyCourse.Models.Services.Infrastructure;
using src.Models.Options;
using src.Models.Services.Application;

namespace CorsoDotNet
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()

            #if DEBUG
            .AddRazorRuntimeCompilation()
            #endif
            ;

            // services.AddTransient<ICourseService, AdoNetCourseService>();
            services.AddTransient<ICourseService, EfCoreCourseService>();
            services.AddTransient<IDatabaseAccessor, SqliteDatabaseAccessor>();
            services.AddTransient<IErrorViewSelectorService, ErrorViewSelectorService>();
            // services.AddScoped<MyCourseDbContext>();
            // services.AddDbContext<MyCourseDbContext>();
            services.AddDbContextPool<MyCourseDbContext>(optionsBuilder => {
                string connectionString = Configuration.GetSection("ConnectionStrings").GetValue<string>("Default");
                optionsBuilder.UseSqlite(connectionString);
            });

            //Options

            services.Configure<ConnectionStringsOptions>(Configuration.GetSection("ConnectionStrings"));
            services.Configure<CoursesOptions>(Configuration.GetSection("Courses"));
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
            
            else{
                app.UseExceptionHandler("/Error"); 
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
