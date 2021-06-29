using System;
using System.Windows;
using Auftragsverwaltung.Application.Common;
using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Application.Service;
using Auftragsverwaltung.Domain.Article;
using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Domain.Customer;
using Auftragsverwaltung.Infrastructure.Article;
using Auftragsverwaltung.Infrastructure.Common;
using Auftragsverwaltung.Infrastructure.Customer;
using Auftragsverwaltung.Infrastructure.Article;
using Auftragsverwaltung.WPF.State.Navigators;
using Auftragsverwaltung.WPF.ViewModels;
using Auftragsverwaltung.WPF.ViewModels.Factories;
using Microsoft.Extensions.DependencyInjection;
using Auftragsverwaltung.Domain.Article;

namespace Auftragsverwaltung.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IServiceProvider serviceProvider = CreateServiceProvider();

            Window window = serviceProvider.GetRequiredService<MainWindow>();
            window.Show();

            base.OnStartup(e);
        }

        private IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<AppDbContextFactory>();
            services.AddSingleton<IAppRepository<Customer>, CustomerRepository>();
            services.AddSingleton<IAppRepository<Article>, ArticleRepository>();
            services.AddSingleton<ICustomerService, CustomerService>();
            services.AddSingleton<IArticleService, ArticleService>();

            services.AddSingleton<IAppViewModelAbstractFactory, AppViewModelAbstractFactory>();
            services.AddSingleton<IAppViewModelFactory<HomeViewModel>, HomeViewModelFactory>();
            services.AddSingleton<IAppViewModelFactory<CustomerViewModel>, CustomerViewModelFactory>();
            services.AddSingleton<IAppViewModelFactory<ArticleViewModel>, ArticleViewModelFactory>();
            services.AddSingleton<IAppViewModelFactory<OrderViewModel>, OrderViewModelFactory>();

            services.AddScoped<INavigator, Navigator>();
            services.AddScoped<MainViewModel>();

            services.AddScoped<MainWindow>(s => new MainWindow(s.GetRequiredService<MainViewModel>()));

            return services.BuildServiceProvider();
        }
    }
}
