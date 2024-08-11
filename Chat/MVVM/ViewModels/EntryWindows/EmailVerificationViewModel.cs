using Chat.MVVM.Core;
using Chat.MVVM.Models.Handlers;
using Chat.MVVM.Models.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.MVVM.ViewModels.EntryWindows
{
    internal class EmailVerificationViewModel : EntryWindowsViewModelBase
    {
        // ============= Properties for Binding =============
        private string _code = string.Empty;

        [Required(ErrorMessage = "Can not be empty")]
        [RegularExpression(@"^[A-Z0-9]{6}$", ErrorMessage = "Code is invalid")]
        public string Code
        {
            get => _code;
            set => SetValidatedField(ref _code, value, nameof(Code));
        }


        // ============= Commands =============
        public RelayCommand VerifyCommand { get; set; }
        public RelayCommand NavigateToRegistrationCommand { get; set; }


        public EmailVerificationViewModel()
        {
            NavigateToRegistrationCommand = new RelayCommand((o) => Navigator.NavigateTo<RegistrationViewModel>());

            VerifyCommand = new RelayCommand(Verify, CanVerify);
        }


        // ============= private methods =============
        private void Verify(object obj)
        {
            EmailVerifier.Verify(Code);
        }

        private bool CanVerify(object obj)
        {
            return !HasErrors
                && !string.IsNullOrEmpty(_code);
        }


        // ============= default methods =============
        public override void ResetData()
        {
            _code = string.Empty;
        }
    }
}
