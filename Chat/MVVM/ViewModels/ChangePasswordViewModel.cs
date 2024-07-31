using Chat.MVVM.Core;
using Chat.MVVM.Models.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.MVVM.ViewModels
{
    internal class ChangePasswordViewModel : ViewModelBase
    {
        private string _password = string.Empty;

        [Required(ErrorMessage = "Can not be empty")]
        [MinLength(8, ErrorMessage = "Password should contain at least 8 symbols")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-zA-Z]).*", ErrorMessage = "Password should contain at least one number and one letter")]
        public string Password
        {
            get => _password;
            set => SetValidatedField(ref _password, value, nameof(Password));
        }


        private string _confirmationPassword = string.Empty;

        [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match")]
        public string ConfirmationPassword
        {
            get => _confirmationPassword;
            set => SetValidatedField(ref _confirmationPassword, value, nameof(ConfirmationPassword));
        }

        public RelayCommand SubmitCommand { get; set; }
        public RelayCommand NavigateToLoginCommand { get; set; }


        public ChangePasswordViewModel()
        {
            NavigateToLoginCommand = new RelayCommand(o => Navigator.NavigateTo<LoginViewModel>());

            SubmitCommand = new RelayCommand(Submit, CanSubmit);
        }

        private void Submit(object obj)
        {
            PasswordChanger.ChangePassword(Password);
        }

        private bool CanSubmit(object obj) 
        {
            return !HasErrors
                && !string.IsNullOrEmpty(Password)
                && !string.IsNullOrEmpty(ConfirmationPassword);
        }
    }
}
