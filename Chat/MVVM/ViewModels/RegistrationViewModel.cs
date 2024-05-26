using Chat.MVVM.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.MVVM.ViewModels
{
    public class RegistrationViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        public INavigationService NavigationService
        {
            get => _navigationService;
            set => SetField(ref _navigationService, value);
        }

        public RelayCommand NavigateToLoginCommand { get; set; }

        public RegistrationViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;

            NavigateToLoginCommand = new RelayCommand(o => NavigationService.NavigateTo<LoginViewModel>());
        }
    }
}
