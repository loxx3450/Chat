﻿using Chat.MVVM.Core;
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
    internal class RecoveryCodeConfirmationViewModel : EntryWindowsViewModelBase
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
        public RelayCommand ConfirmCommand { get; set; }
        public RelayCommand NavigateToLoginCommand { get; set; }
        public RelayCommand NavigateToRegistrationCommand { get; set; }


        public RecoveryCodeConfirmationViewModel()
        {
            NavigateToLoginCommand = new RelayCommand((o) => Navigator.NavigateTo<LoginViewModel>());
            NavigateToRegistrationCommand = new RelayCommand((o) => Navigator.NavigateTo<RegistrationViewModel>());

            ConfirmCommand = new RelayCommand(Confirm, CanConfirm);
        }


        // ============= private methods =============
        private void Confirm(object obj)
        {
            CodeVerifierForResetPassword.VerifyCode(Code);
        }

        private bool CanConfirm(object obj) 
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
