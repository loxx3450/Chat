using Chat.Core;
using Chat.MVVM.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using Chat.MVVM.Models.Services;
using Chat.MVVM.Models.Handlers;

namespace Chat.MVVM.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public bool RememberUser { get; set; }


        private string _email = string.Empty;

        [Required(ErrorMessage = "Can not be empty")]
        [RegularExpression(@"^\w+@[a-zA-Z]+\.[a-zA-Z]+", ErrorMessage = "Email is invalid")]
        public string Email
        {
            get => _email;
            set => SetValidatedField(ref _email, value, nameof(Email));
        }


        private string _password = string.Empty;

        [Required(ErrorMessage = "Can not be empty")]
        [MinLength(8, ErrorMessage = "Password should contain at least 8 symbols")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-zA-Z]).*", ErrorMessage = "Password should contain at least one number and one letter")]
        public string Password
        {
            get => _password;
            set => SetValidatedField(ref _password, value, nameof(Password));
        }


        public RelayCommand SignInCommand { get; set; }
        public RelayCommand NavigateToRegistrationCommand { get; set; }
        public RelayCommand NavigateToRecoveryCommand { get; set; }

        public LoginViewModel()
        {
            NavigateToRegistrationCommand = new RelayCommand(o => Navigator.NavigateTo<RegistrationViewModel>());
            NavigateToRecoveryCommand = new RelayCommand(o => Navigator.NavigateTo<RecoveryViewModel>());

            SignInCommand = new RelayCommand(SignIn, CanSignIn);
        }

        private void SignIn(object obj)
        {
            SigningInService.SignIn(Email, Password, RememberUser);
        }

        private bool CanSignIn(object obj)
        {
            return !HasErrors
                && !string.IsNullOrEmpty(_email)
                && !string.IsNullOrEmpty(_password);
        }

        public override void ResetData()
        {
            _email = string.Empty;
            _password = string.Empty;
        }
    }
}
