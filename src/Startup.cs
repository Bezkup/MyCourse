using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models.Services.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using src.Models.Services.Application;
using src.Models.Services;
using src.Models.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc;

namespace src
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
            services.AddResponseCaching();

            services.AddMvc(options => 
            {   
                var homeProfile = new CacheProfile();
                // homeProfile.Duration = Configuration.GetValue<int>("ResponseCache:Home:Duration");
                // homeProfile.Location = Configuration.GetValue<ResponseCacheLocation>("ResponseCache:Home:Location");
                //homeProfile.VaryByQueryKeys = new string[] {"page"};
                
                Configuration.Bind("ResponseCache:Home", homeProfile);
                options.CacheProfiles.Add("Home", homeProfile);
            });

            services.AddControllersWithViews()

            #if DEBUG
            .AddRazorRuntimeCompilation()
            #endif
            ;

            // services.AddTransient<ICourseService, AdoNetCourseService>();
            services.AddTransient<ICourseService, EfCoreCourseService>();
            services.AddTransient<IDatabaseAccessor, SqliteDatabaseAccessor>();
            services.AddTransient<IErrorViewSelectorService, ErrorViewSelectorService>();
            services.AddTransient<ICachedCourseService, DistributedCacheCourseService>();
            // services.AddScoped<MyCourseDbContext>();
            // services.AddDbContext<MyCourseDbContext>();
            services.AddDbContextPool<MyCourseDbContext>(optionsBuilder => {
                string connectionString = Configuration.GetSection("ConnectionStrings").GetValue<string>("Default");
                optionsBuilder.UseSqlite(connectionString);
            });


            #region Configurazione del servizio di cache distribuita

            services.AddStackExchangeRedisCache(options =>
            {
                Configuration.Bind("DistributedCache:Redis", options);
            });
            //Options
            #endregion

            services.Configure<ConnectionStringsOptions>(Configuration.GetSection("ConnectionStrings"));
            services.Configure<CoursesOptions>(Configuration.GetSection("Courses"));
            services.Configure<MemoryCacheOptions>(Configuration.GetSection("MemoryCache"));
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

            app.UseResponseCaching();

            app.UseRouting();

            //app.UseMvcWithDefaultRoute();
            
            app.UseEndpoints(route => {
                route.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
