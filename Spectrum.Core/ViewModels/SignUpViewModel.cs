using MvvmCross.Commands;
using MvvmCross.Navigation;
using Spectrum.Repository.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Core.ViewModels
{
    public class SignUpViewModel : BaseViewModel
    {
        private IDataAccessService _dataAccessService;
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
            set => SetProperty(ref _lastName, value);
        }
        public string Password
        {
            get => _password;
            set => SetProperty(ref _lastName, value);
        }
        public DateTime ServiceDate
        {
            get => _serviceDate;
            set => SetProperty(ref _serviceDate, value);
        }
        public IMvxAsyncCommand SignUpCommand { get; private set; }

        public SignUpViewModel(IMvxNavigationService navigationService, IDataAccessService dataAccessService) : base(navigationService)
        {
            _dataAccessService = dataAccessService;
            SignUpCommand = new MvxAsyncCommand(SignUpAsync);
        }

        private async Task SignUpAsync()
        {
            Console.WriteLine(ServiceDate);
            await _navigationService.Navigate<SuccessLoginViewModel>();
        }
    }
}
