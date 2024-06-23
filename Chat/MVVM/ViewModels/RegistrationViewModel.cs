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
        public RelayCommand NavigateToLoginCommand { get; set; }

        public RegistrationViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            NavigateToLoginCommand = new RelayCommand(o => NavigationService.NavigateTo<LoginViewModel>());
        }
    }
}
