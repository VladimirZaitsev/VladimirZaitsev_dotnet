using AutoMapper;
using BLL.Automapper;
using BLL.Interfaces;
using DAL.Core;
using DAL.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebUI.Services;

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
                options.UseSqlServer(connectionString));

            services.AddAutoMapper(typeof(AutomapperBLLConfig));

            services.Scan(scan => scan
                .FromAssembliesOf(typeof(IStore<>))
                    .AddClasses(classes => classes.AssignableTo(typeof(IStore<>)))
                    .AsImplementedInterfaces());

            services.Scan(scan => scan
                .FromAssembliesOf(typeof(IStore<>))
                    .AddClasses(classes => classes.AssignableTo(typeof(IStore<>)))
                    .AsImplementedInterfaces());

            services.Scan(scan => scan
                .FromAssembliesOf(typeof(IService<>))
                    .AddClasses(classes => classes.AssignableTo(typeof(IService<>)))
                    .AsImplementedInterfaces());

            services.AddTransient<StudentService>();
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

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