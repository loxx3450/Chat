using Chat.MVVM;
using Chat.MVVM.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Chat
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();

            //Adding MainWindow
            services.AddSingleton<MainWindow>(provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });

            //Adding ViewModels
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<RegistrationViewModel>();
            services.AddSingleton<LoginViewModel>();
            services.AddSingleton<RecoveryViewModel>();

            //Adding Services
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IServiceProvider, ServiceProvider>();

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            //Setting MainWindow as StartWindow
            _serviceProvider.GetRequiredService<MainWindow>().Show();

            base.OnStartup(e);
        }
    }

}
