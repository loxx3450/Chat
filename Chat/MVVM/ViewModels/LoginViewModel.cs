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
            set => SetValidatedField(ref _email, value, nameof(Email));
        }

        private string _password;

        [Required(ErrorMessage = "Can not be empty")]
        [MinLength(8, ErrorMessage = "Password should contain at least 8 symbols")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-zA-Z]).*", ErrorMessage = "Password should contain at least one number and one letter")]
        public string Password
        {
            get => _password;
            set => SetValidatedField(ref _password, value, nameof(Password));
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
