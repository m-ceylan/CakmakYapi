using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cakmak.Yapi.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cakmak.Yapi.Presentation
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
            string connectionString = Configuration.GetConnectionString("mongoDB");
            services.ConfigureRepositories(connectionString);
            services.AddControllersWithViews();
            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 60000000;
            });
            //services.AddControllers()./*AddNewtonsoftJson*/();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.Use(async (ctx, next) =>
            {
                await next();

                if (ctx.Response.StatusCode == 404 && !ctx.Response.HasStarted)
                {
                    //Re-execute the request so the user gets the error page
                    string originalPath = ctx.Request.Path.Value;
                    ctx.Items["originalPath"] = originalPath;
                    ctx.Request.Path = "/404";
                    await next();
                }
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                #region Admin
                endpoints.MapAreaControllerRoute(
                        name: "Admin",
                        areaName: "Admin",
                        pattern: "admin/{controller=HomeM}/{action=Index}/{id?}"
                       ); 
                #endregion

                #region Ýletiþim
                endpoints.MapControllerRoute
        (
            name: "contact",
            pattern: "iletisim",
            defaults: new { controller = "Contact", action = "Index" }
        ); 
                #endregion

                #region Katalog
                endpoints.MapControllerRoute
                 (
                     name: "catalog",
                     pattern: "katalog",
                     defaults: new { controller = "Catalog", action = "Index" }
                 );

                #endregion

                #region Çalýþmalarýmýz
                endpoints.MapControllerRoute
               (
                   name: "work/detail",
                   pattern: "calismalarimiz/{id}",
                   defaults: new { controller = "Work", action = "Detail" }
               );

                endpoints.MapControllerRoute
              (
                  name: "work",
                  pattern: "calismalarimiz",
                  defaults: new { controller = "Work", action = "Index" }
              );

                #endregion

                #region Hizmetlerimiz
                endpoints.MapControllerRoute
                    (
                        name: "services/detail",
                        pattern: "hizmetlerimiz/{id}",
                        defaults: new { controller = "Services", action = "Detail" }
                    );

                endpoints.MapControllerRoute
               (
                   name: "services",
                   pattern: "hizmetlerimiz",
                   defaults: new { controller = "Services", action = "Index" }
               ); 
                #endregion

                #region Hakkýmýz
                endpoints.MapControllerRoute
        (
            name: "about",
            pattern: "hakkimizda",
            defaults: new { controller = "About", action = "Index" }
        ); 
                #endregion

                #region Anasayfa
                endpoints.MapControllerRoute(
                         name: "home",
                         pattern: "anasayfa",
                         defaults: new { controller = "Home", action = "Index" }); 
                #endregion

                #region Default
                endpoints.MapControllerRoute(
                            name: "default",
                            pattern: "{controller=Home}/{action=Index}/{id?}");
            }); 
            #endregion
        }
    }
}
