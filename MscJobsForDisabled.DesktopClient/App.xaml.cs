using Microsoft.Extensions.DependencyInjection;
using MscJobsForDisabled.ApplicationServices.GetJobListUseCase;
using MscJobsForDisabled.ApplicationServices.Ports.Cache;
using MscJobsForDisabled.ApplicationServices.Repositories;
using MscJobsForDisabled.DesktopClient.InfrastructureServices.ViewModels;
using MscJobsForDisabled.DomainObjects;
using MscJobsForDisabled.DomainObjects.Ports;
using MscJobsForDisabled.InfrastructureServices.Cache;
using MscJobsForDisabled.InfrastructureServices.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MscJobsForDisabled.DesktopClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDomainObjectsCache<Job>, DomainObjectsMemoryCache<Job>>();
            services.AddSingleton<NetworkJobRepository>(
                x => new NetworkJobRepository("localhost", 80, useTls: false, x.GetRequiredService<IDomainObjectsCache<Job>>())
            );
            services.AddSingleton<CachedReadOnlyJobRepository>(
                x => new CachedReadOnlyJobRepository(
                    x.GetRequiredService<NetworkJobRepository>(),
                    x.GetRequiredService<IDomainObjectsCache<Job>>()
                )
            );
            services.AddSingleton<IReadOnlyJobRepository>(x => x.GetRequiredService<CachedReadOnlyJobRepository>());
            services.AddSingleton<IGetJobListUseCase, GetJobListUseCase>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MainWindow>();
        }

        private void OnStartup(object sender, StartupEventArgs args)
        {
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}
