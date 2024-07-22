using Chat.MVVM.ViewModels;
using Chat.MVVM;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Core
{
    public static class ServiceProvider
    {
        private static IServiceProvider _serviceProvider = null;

        public static IServiceProvider GetServiceProvider()
        {
            if (_serviceProvider == null) 
            {
                InitializeServiceProvider();
            }

            return _serviceProvider;
        }

        public static T GetRequiredService<T>() 
            where T : notnull
        {
            return GetServiceProvider().GetRequiredService<T>();
        }

        public static T? GetService<T>()
            where T : notnull
        {
            return GetServiceProvider().GetService<T>();
        }

        private static void InitializeServiceProvider()
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
            services.AddSingleton<ChatViewModel>();

            //Adding Services
            services.AddSingleton<INavigationService, NavigationService>();

            _serviceProvider = services.BuildServiceProvider();
        }
    }
}
