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

namespace StudentApi
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
            services.AddControllers();
            services.Scan(scan => scan
                .FromAssembliesOf(typeof(IStudentService))
                    .AddClasses(classes => classes.AssignableTo(typeof(IStudentService)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());
            var connectionString = Configuration.GetConnectionString("local");
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connectionString), ServiceLifetime.Scoped);

            services.Scan(scan => scan
                .FromAssembliesOf(typeof(IStore<>))
                    .AddClasses(classes => classes.AssignableTo(typeof(IStore<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

            services.AddAutoMapper(typeof(AutomapperBLLConfig));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
