using AutoMapper;
using BLL.Automapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Core;
using DAL.Dtos;
using DAL.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using WebUI.Identity;

namespace WebUI
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
            services.AddControllersWithViews();

            var connectionString = Configuration.GetConnectionString("local");
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connectionString), ServiceLifetime.Scoped);

            services.AddAutoMapper(typeof(AutomapperBLLConfig));

            services.Scan(scan => scan
                .FromAssembliesOf(typeof(IStore<>))
                    .AddClasses(classes => classes.AssignableTo(typeof(IStore<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

            services.Scan(scan => scan
                .FromAssembliesOf(typeof(IService<>))
                    .AddClasses(classes => classes.AssignableTo(typeof(IService<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

            services.Scan(scan => scan
                .FromAssembliesOf(typeof(Startup))
                    .AddClasses(classes => classes.Where(cls => cls.Name.EndsWith("Facade")))
                    .AsSelf()
                    .WithTransientLifetime());

            services.Scan(scan => scan
                .FromAssembliesOf(typeof(IIdentityService))
                    .AddClasses(classes => classes.AssignableTo(typeof(IIdentityService)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

            services.AddSingleton<DBIntializer>();

            services.AddIdentity<UserDto, IdentityRole>()
              .AddEntityFrameworkStores<ApplicationContext>()
              .AddDefaultTokenProviders();

            services.AddHttpClient("StudentApi", options => options.BaseAddress = new Uri(Configuration["ApiUrls:StudentApi"]));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                var dbInitilizer = app?.ApplicationServices.GetService<DBIntializer>();
                dbInitilizer.Seed().GetAwaiter().GetResult();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
