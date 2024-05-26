using Chat.MVVM.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.MVVM.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        public INavigationService NavigationService
        {
            get => _navigationService;
            set => SetField<INavigationService>(ref _navigationService, value);
        }

        public MainViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;

            NavigationService.NavigateTo<LoginViewModel>();
        }
    }
}
