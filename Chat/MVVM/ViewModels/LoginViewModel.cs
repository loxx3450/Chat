using Chat.MVVM.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Chat.MVVM.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _email;

        [Required(ErrorMessage = "Can not be empty")]
        [RegularExpression(@"^\w+@[a-zA-Z]+\.[a-zA-Z]+", ErrorMessage = "Email is invalid")]
        public string Email
        {
            get => _email;
            set
            {
                ValidateProperty(value, nameof(Email));
                SetField(ref _email, value);
            }
        }

        private string _password; 
        public string Password
        {
            get => _password;
            set => SetField(ref _password, value);
        }

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
