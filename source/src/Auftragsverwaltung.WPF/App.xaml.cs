using System;
using System.Windows;
using Auftragsverwaltung.Application.Common;
using Auftragsverwaltung.Application.Dtos;
using Auftragsverwaltung.Application.Service;
using Auftragsverwaltung.Domain.Common;
using Auftragsverwaltung.Domain.Customer;
using Auftragsverwaltung.Infrastructure.Common;
using Auftragsverwaltung.Infrastructure.Customer;
using Auftragsverwaltung.WPF.State.Navigators;
using Auftragsverwaltung.WPF.ViewModels;
using Auftragsverwaltung.WPF.ViewModels.Factories;
using Microsoft.Extensions.DependencyInjection;

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
            window.DataContext = serviceProvider.GetRequiredService<MainViewModel>();
            window.Show();

            base.OnStartup(e);
        }

        private IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();


            services.AddSingleton<AppDbContextFactory>();
            services.AddSingleton<IAppRepository<Customer>, CustomerRepository>();
            services.AddSingleton<ICustomerService, CustomerService>();
            services.AddScoped<INavigator, Navigator>();


            services.AddTransient(Test.CreateHomeViewModel);
            services.AddTransient<CustomerViewModel>();
            services.AddTransient<ArticleViewModel>();
            services.AddTransient<OrderViewModel>();
            services.AddTransient<MainViewModel>();

            services.AddSingleton<CreateViewModel<HomeViewModel>>(services => () => services.GetRequiredService<HomeViewModel>());
            services.AddSingleton<CreateViewModel<CustomerViewModel>>(services => () => services.GetRequiredService<CustomerViewModel>());
            services.AddSingleton<CreateViewModel<ArticleViewModel>>(services => () => services.GetRequiredService<ArticleViewModel>());
            services.AddSingleton<CreateViewModel<OrderViewModel>>(services => () => services.GetRequiredService<OrderViewModel>());
            services.AddSingleton<CreateViewModel<MainViewModel>>(services => () => services.GetRequiredService<MainViewModel>());

            services.AddSingleton<IAppViewModelAbstractFactory, AppViewModelAbstractFactory>();

            services.AddSingleton<MainWindow>(s => new MainWindow(s.GetRequiredService<MainViewModel>()));

            return services.BuildServiceProvider();
        }
    }

    public static class Test
    {
        public static HomeViewModel CreateHomeViewModel(IServiceProvider services)
        {
            return new HomeViewModel();
        }
    }

}
