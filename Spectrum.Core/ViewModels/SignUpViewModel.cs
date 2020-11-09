﻿using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Spectrum.Repository.Abstractions;
using Spectrum.Repository.Entities;
using System;
using System.Threading.Tasks;

namespace Spectrum.Core.ViewModels
{
    public class SignUpViewModel : BaseViewModel
    {
        private readonly IDataAccessService<User> _dataAccessService;
        private readonly IUserDialogs _userDialogsService;
        private string _firstName;
        private string _lastName;
        private string _userName;
        private string _password;
        private string _phoneNumberNotFormated;
        private bool _isPhoneNumberAlreadyFormatted;
        private string _phoneNumber;
        private DateTime _serviceDate;
        private bool _areFieldsPopulated;

        public bool AreFieldsPopulated
        {
            get => _areFieldsPopulated;
            set => SetProperty(ref _areFieldsPopulated, value);
        }
        public string FirstName
        {
            get => _firstName;
            set
            {
                AreFieldsPopulated = AreFieldsRight();

                SetProperty(ref _firstName, value);
            }
        }
        public string LastName
        {
            get => _lastName;
            set
            {
                AreFieldsPopulated = AreFieldsRight();

                SetProperty(ref _lastName, value);
            }
        }
        public string UserName
        {
            get => _userName;
            set
            {
                AreFieldsPopulated = AreFieldsRight();

                SetProperty(ref _userName, value);
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                AreFieldsPopulated = AreFieldsRight();

                SetProperty(ref _password, value);
            }
        }
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                AreFieldsPopulated = AreFieldsRight();

                if (Utils.Utils.IsPhoneNumberValid(value) && !_isPhoneNumberAlreadyFormatted)
                {
                    _phoneNumberNotFormated = value;
                    _isPhoneNumberAlreadyFormatted = true;
                }

                if (Utils.Utils.IsPhoneNumberValid(value))
                {
                    value = Utils.Utils.FormatPhoneNumber(value);
                }

                if (value.Length <= 14)
                {
                    SetProperty(ref _phoneNumber, value);
                }
                else
                {
                    SetProperty(ref _phoneNumber, Utils.Utils.FormatPhoneNumber(_phoneNumberNotFormated));
                    RaisePropertyChanged();
                }
            }
        }
        public DateTime ServiceDate
        {
            get => _serviceDate;
            set => SetProperty(ref _serviceDate, value);
        }
        public IMvxAsyncCommand SignUpCommand { get; private set; }
        public IMvxAsyncCommand BackCommand { get; private set; }

        public SignUpViewModel(IMvxNavigationService navigationService, IDataAccessService<User> dataAccessService, IUserDialogs userDialogs) : base(navigationService)
        {
            AreFieldsPopulated = false;
            _isPhoneNumberAlreadyFormatted = false;
            _userDialogsService = userDialogs;
            _dataAccessService = dataAccessService;
            SignUpCommand = new MvxAsyncCommand(SignUpAsync);
            BackCommand = new MvxAsyncCommand(Back);
        }

        private bool AreFieldsRight()
        {
            try
            {
                var isPhoneOk = _isPhoneNumberAlreadyFormatted || Utils.Utils.IsPhoneNumberValid(PhoneNumber);

                if (Utils.Utils.IsNameValid(FirstName) && Utils.Utils.IsNameValid(LastName) && Utils.Utils.IsNameValid(UserName) && Utils.Utils.IsPasswordValid(Password) && isPhoneOk)
                {
                    return true;
                }
            }
            catch (ArgumentException)
            { }

            return false;
        }

        private async Task SignUpAsync()
        {
            var user = new User()
            {
                FirstName = _firstName,
                LastName = _lastName,
                Password = _password,
                Start = _serviceDate,
                PhoneNumber = _phoneNumberNotFormated,
                UserName = _userName
            };

            var affected = await _dataAccessService.SaveEntityAsync(user);

            if (affected == 0)
            {
                _userDialogsService.Alert("One or more fields are incorrect.");

                return;
            }

            await _navigationService.Navigate<SuccessSignupViewModel>();
        }

        private async Task Back()
        {
            await _navigationService.Close(this);
            await _navigationService.Navigate<SignInViewModel>();
        }
    }
}
