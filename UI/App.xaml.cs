using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;
using UI.Services;
using UI.ViewModels;
using UI.Views;

namespace UI
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            var host = Host.CreateDefaultBuilder().ConfigureServices(ConfigureServices).Build();

            ServiceProvider = host.Services;
        }

        protected async override void OnStartup(StartupEventArgs e)
        {
            var navigation = ServiceProvider.GetService<Services.NavigationService>();
            await navigation.ShowAsync(Constant.Windows.MainWindow);

            base.OnStartup(e);
        }

        private void ConfigureServices(HostBuilderContext hostBuilderContext, IServiceCollection services)
        {
            services.AddScoped<Services.NavigationService>(serviceProvider =>
            {
                var navigationService = new Services.NavigationService(serviceProvider);
                navigationService.Configure(Constant.Windows.MainWindow, typeof(MainWindow));
                navigationService.Configure(Constant.Windows.PendingFlights, typeof(PendingFlights));
                navigationService.Configure(Constant.Windows.ProcessingFlights, typeof(ProcessingFlights));
                navigationService.Configure(Constant.Windows.CompletedFlights, typeof(CompletedFlights));
                navigationService.Configure(Constant.Windows.Legs, typeof(Legs));

                return navigationService;
            });

            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MainWindow>();

            services.AddSingleton<PendingFlightsViewModel>();
            services.AddSingleton<PendingFlights>();

            services.AddSingleton<ProcessingFlightsViewModel>();
            services.AddSingleton<ProcessingFlights>();

            services.AddSingleton<CompletedFlightsViewModel>();
            services.AddSingleton<CompletedFlights>();

            services.AddSingleton<LegsViewModel>();
            services.AddSingleton<Legs>();

            services.AddScoped<IFlightsApiService, FlightsApiService>();
            services.AddScoped<ILegsApiService, LegsApiService>();
        }
    }
}
