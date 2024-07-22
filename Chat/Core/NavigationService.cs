using Chat.MVVM.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui;

namespace Chat.Core
{
    public interface INavigationService
    {
        ViewModelBase CurrentView { get; }

        void NavigateTo<T>() where T : ViewModelBase;
    }

    public class NavigationService : ObservableObject, INavigationService
    {
        private ViewModelBase _currentView;

        public ViewModelBase CurrentView
        {
            get => _currentView;
            set => SetField<ViewModelBase>(ref _currentView, value);
        }

        public void NavigateTo<TViewModel>() where TViewModel : ViewModelBase
        {
            CurrentView = ServiceProvider.GetRequiredService<TViewModel>();
        }
    }
}
