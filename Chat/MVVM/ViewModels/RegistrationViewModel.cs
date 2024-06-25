using Chat.MVVM.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.MVVM.ViewModels
{
    public class RegistrationViewModel : ViewModelBase
    {
        private string _username;

        [Required(ErrorMessage = "Can not be empty")]
        [RegularExpression(@"^[^\s]+", ErrorMessage = "Username can not contain spaces")]
        public string Username
        {
            get => _username;
            set
            {
                //TODO: SetValidatedProperty
                ValidateProperty(value, nameof(Username));
                SetField(ref _username, value);
            }
        }


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

        [Required(ErrorMessage = "Can not be empty")]
        [MinLength(8, ErrorMessage = "Password should contain at least 8 symbols")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-zA-Z]).*", ErrorMessage = "Password should contain at least one number and one letter")]
        public string Password
        {
            get => _password;
            set
            {
                ValidateProperty(value, nameof(Password));
                SetField(ref _password, value);
            }
        }


        private string _confirmationPassword;

        [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match")]
        public string ConfirmationPassword
        {
            get => _confirmationPassword;
            set
            {
                ValidateProperty(value, nameof(ConfirmationPassword));
                SetField(ref _confirmationPassword, value);
            }
        }


        public RelayCommand NavigateToLoginCommand { get; set; }

        public RegistrationViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            NavigateToLoginCommand = new RelayCommand(o => NavigationService.NavigateTo<LoginViewModel>());
        }
    }
}
