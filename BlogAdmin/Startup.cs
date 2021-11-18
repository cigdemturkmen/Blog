using Blog.Data;
using Blog.Services.Concrete;
using Blog.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAdmin
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BlogDbContext>(option =>
            {
                option.UseSqlServer("Server=.;Database=BlogDev;User Id=sa;Password=123;");
                //ba�ka projeler dataya ba�l� ama data onlara ba�l� de�il
            });

            services.AddScoped<ICategoryRepository, CategoryRepository>(); //instance al�nca ne verece�ini s�yleyen yer.

            services.AddControllersWithViews(); // MVC
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern : "{controller}/{action}/{id?}",
                    defaults: new { controller = "home", action = "Index"}
                    );
            });

            app.UseStaticFiles(); // contextteki �eylere eri�ebilmek i�in bunu yapman gerekiyor.
        }
    }
}