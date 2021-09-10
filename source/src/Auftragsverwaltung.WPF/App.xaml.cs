using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Application.Mapper;
using Auftragsverwaltung.Application.Service;
using Auftragsverwaltung.Application.Validators;
using Auftragsverwaltung.Domain.Article;
using Auftragsverwaltung.Domain.ArticleGroup;
using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Domain.Customer;
using Auftragsverwaltung.Domain.Order;
using Auftragsverwaltung.Infrastructure.Article;
using Auftragsverwaltung.Infrastructure.ArticleGroup;
using Auftragsverwaltung.Infrastructure.Common;
using Auftragsverwaltung.Infrastructure.Customer;
using Auftragsverwaltung.Infrastructure.Order;
using Auftragsverwaltung.WPF.State.Navigators;
using Auftragsverwaltung.WPF.ViewModels;
using Auftragsverwaltung.WPF.ViewModels.Factories;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using Auftragsverwaltung.Application.Serializer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Auftragsverwaltung.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = CreateHostBuilder().Build();
        }

        public static IHostBuilder CreateHostBuilder(string[] args = null)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(c =>
                {
                    c.AddJsonFile("appsettings.json");
                })
                .ConfigureServices((context, services) =>
                {
                    string connectionString = context.Configuration.GetConnectionString("default");
                    services.AddDbContext<AppDbContext>(o =>
                        o.UseSqlServer(connectionString));

                    services.AddSingleton<IAppRepository<Customer>, CustomerRepository>();
                    services.AddSingleton<IAppRepository<Article>, ArticleRepository>();
                    services.AddSingleton<IArticleGroupRepository, ArticleGroupRepository>();
                    services.AddSingleton<IOrderRepository, OrderRepository>();
                    services.AddSingleton<ICustomerService, CustomerService>();
                    services.AddSingleton<IArticleService, ArticleService>();
                    services.AddSingleton<IArticleGroupService, ArticleGroupService>();
                    services.AddSingleton<IOrderService, OrderService>();

                    services.AddSingleton<IAppViewModelAbstractFactory, AppViewModelAbstractFactory>();
                    services.AddSingleton<IAppViewModelFactory<HomeViewModel>, HomeViewModelFactory>();
                    services.AddSingleton<IAppViewModelFactory<CustomerViewModel>, CustomerViewModelFactory>();
                    services.AddSingleton<IAppViewModelFactory<ArticleViewModel>, ArticleViewModelFactory>();
                    services.AddSingleton<IAppViewModelFactory<ArticleGroupViewModel>, ArticleGroupViewModelFactory>();
                    services.AddSingleton<IAppViewModelFactory<OrderViewModel>, OrderViewModelFactory>();

                    services.AddScoped<INavigator, Navigator>();
                    services.AddScoped<MainViewModel>();

                    services.AddSingleton<IValidator<CustomerDto>, CustomerValidator>();
                    services.AddSingleton<ISerializer<CustomerDto>, CustomerSerializer>();

                    services.AddScoped<MainWindow>(s => new MainWindow(s.GetRequiredService<MainViewModel>()));

                    var mapperConfig = new MapperConfiguration(mc =>
                    {
                        mc.AddProfile(new MappingProfile());
                    });

                    IMapper mapper = mapperConfig.CreateMapper();
                    services.AddSingleton(mapper);
                });
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            Window window = _host.Services.GetRequiredService<MainWindow>();
            window.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StartAsync();
            _host.Dispose();

            base.OnExit(e);
        }
    }
}
