using Acr.UserDialogs;
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
                AreFieldsPopulated = CheckThatFieldAreRight();

                SetProperty(ref _firstName, value);
            }
        }
        public string LastName
        {
            get => _lastName;
            set
            {
                AreFieldsPopulated = CheckThatFieldAreRight();

                SetProperty(ref _lastName, value);
            }
        }
        public string UserName
        {
            get => _userName;
            set
            {
                AreFieldsPopulated = CheckThatFieldAreRight();

                SetProperty(ref _userName, value);
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                AreFieldsPopulated = CheckThatFieldAreRight();

                SetProperty(ref _password, value);
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
            _userDialogsService = userDialogs;
            _dataAccessService = dataAccessService;
            SignUpCommand = new MvxAsyncCommand(SignUpAsync);
            BackCommand = new MvxAsyncCommand(Back);
        }

        private bool CheckThatFieldAreRight()
        {

            if (Utils.Utils.IsNameValid(FirstName) && Utils.Utils.IsNameValid(LastName) && Utils.Utils.IsNameValid(UserName) && Utils.Utils.IsPasswordValid(Password))
                return true;

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
