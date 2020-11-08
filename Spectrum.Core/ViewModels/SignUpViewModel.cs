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

        public string FirstName
        {
            get => _firstName;
            private set => SetProperty(ref _firstName, value);
        }
        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }
        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
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
            _userDialogsService = _userDialogsService;
            _dataAccessService = dataAccessService;
            SignUpCommand = new MvxAsyncCommand(SignUpAsync);
            BackCommand = new MvxAsyncCommand(Back);
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

            if(affected == 0)
            {
                _userDialogsService.Alert("One or more fields are incorrect.");

                return;
            }

            await _navigationService.Navigate<SuccessLoginViewModel>();
        }

        private async Task Back()
        {
            await _navigationService.Close(this);
            await _navigationService.Navigate<SignInViewModel>();
        }
    }
}
