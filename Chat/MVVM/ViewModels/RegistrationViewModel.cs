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
            set => SetValidatedField(ref _username, value, nameof(Username));
        }


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


        //TODO: do i even need this???
        private string _confirmationPassword;

        [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match")]
        public string ConfirmationPassword
        {
            get => _confirmationPassword;
            set => SetValidatedField(ref _confirmationPassword, value, nameof(ConfirmationPassword));
        }


        public RelayCommand NavigateToLoginCommand { get; set; }

        public RegistrationViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            NavigateToLoginCommand = new RelayCommand(o => NavigationService.NavigateTo<LoginViewModel>());
        }
    }
}
