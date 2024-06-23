using Chat.MVVM.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.MVVM.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public RelayCommand NavigateToRegistrationCommand { get; set; }
        public RelayCommand NavigateToRecoveryCommand { get; set; }

        public LoginViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            NavigateToRegistrationCommand = new RelayCommand(o => NavigationService.NavigateTo<RegistrationViewModel>());
            NavigateToRecoveryCommand = new RelayCommand(o => NavigationService.NavigateTo<RecoveryViewModel>());
        }
    }
}
