using AutoMapper;
using BLL.Automapper;
using BLL.Interfaces;
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
using RestEase;
using System;
using WebUI.Api;
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

            services.AddHttpClient("ClassAttendanceAPI")
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(Configuration["ApiUrls:ClassAttendanceAPI"]))
                .AddTypedClient(RestClient.For<IGroupApi>)
                .AddTypedClient(RestClient.For<IStudentApi>)
                .AddTypedClient(RestClient.For<ILecturerApi>)
                .AddTypedClient(RestClient.For<IClassesApi>);

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
