﻿using Chat.Core;
using Chat.MVVM.Core;
using Chat.MVVM.Models.Handlers;
using Chat.MVVM.Models.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Chat.MVVM.ViewModels.EntryWindows
{
    public class RecoveryViewModel : EntryWindowsViewModelBase
    {
        // ============= Properties for Binding =============
        private string _email = string.Empty;

        [Required(ErrorMessage = "Can not be empty")]
        [RegularExpression(@"^\w+@[a-zA-Z]+\.[a-zA-Z]+", ErrorMessage = "Email is invalid")]
        public string Email
        {
            get => _email; 
            set => SetValidatedField(ref _email, value, nameof(Email));
        }


        // ============= Commands =============
        public RelayCommand ResetPasswordCommand { get; set; }
        public RelayCommand NavigateToLoginCommand { get; set; }
        public RelayCommand NavigateToRegistrationCommand { get; set; }


        public RecoveryViewModel()
        {
            NavigateToLoginCommand = new RelayCommand((o) => Navigator.NavigateTo<LoginViewModel>());
            NavigateToRegistrationCommand = new RelayCommand((o) => Navigator.NavigateTo<RegistrationViewModel>());

            ResetPasswordCommand = new RelayCommand(ResetPassword, CanResetPassword);
        }


        // ============= private methods =============
        private void ResetPassword(object obj)
        {
            PasswordResetter.ResetPassword(Email);
        }

        private bool CanResetPassword(object obj) 
        { 
            return !HasErrors
                && !string.IsNullOrEmpty(_email);
        }


        // ============= default methods =============
        public override void ResetData()
        {
            _email = string.Empty;
        }
    }
}
