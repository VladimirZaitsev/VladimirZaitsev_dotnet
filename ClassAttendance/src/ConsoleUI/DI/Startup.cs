using AutoMapper;
using BLL.Automapper;
using BLL.Interfaces;
using ConsoleUI.Automapper;
using ConsoleUI.Contollers.Interfaces;
using ConsoleUI.Views.Interfaces;
using DAL.Core;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace ConsoleUI.DI
{
    public static class Startup
    {
        public static void ConfigureServices(ServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("local");
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddAutoMapper(typeof(AutomapperConsoleConfig), typeof(AutomapperBLLConfig));

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

            services.Scan(scan => scan
                .FromAssembliesOf(typeof(IMenuView<,>))
                    .AddClasses(classes => classes.AssignableTo(typeof(IMenuView<,>)))
                    .AsImplementedInterfaces());

            services.Scan(scan => scan
                .FromAssembliesOf(typeof(IMainController))
                    .AddClasses(classes => classes.AssignableTo(typeof(IMainController)))
                    .AsImplementedInterfaces());

            services.Scan(scan => scan
                .FromAssembliesOf(typeof(ISubController<>))
                    .AddClasses(classes => classes.AssignableTo(typeof(ISubController<>)))
                    .AsImplementedInterfaces());

            services.Scan(scan => scan
                .FromAssembliesOf(typeof(ILecturesMenuView<,>))
                    .AddClasses(classes => classes.AssignableTo(typeof(ILecturesMenuView<,>)))
                    .AsImplementedInterfaces());

            services.Scan(scan => scan
               .FromAssembliesOf(typeof(IGroupMenuView<,>))
                   .AddClasses(classes => classes.AssignableTo(typeof(IGroupMenuView<,>)))
                   .AsImplementedInterfaces());

            services.Scan(scan => scan
               .FromAssembliesOf(typeof(IPrintMenuView))
                   .AddClasses(classes => classes.AssignableTo(typeof(IPrintMenuView)))
                   .AsImplementedInterfaces());
        }
    }
}
