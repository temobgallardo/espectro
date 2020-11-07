using MvvmCross.Commands;
using MvvmCross.Navigation;
using Spectrum.Repository.Abstractions;
using System;
using System.Threading.Tasks;

namespace Spectrum.Core.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IDataAccessService _dataAccessService;
        private string _userName;
        private string _password;

        public string UserName
        {
            get => _userName;
            set { SetProperty(ref _userName, value); }
        }
        public string Password
        {
            get => _password;
            set { SetProperty(ref _password, value); }
        }
        public IMvxCommand LoginCommand { get; private set; }
        public IMvxCommand SignUpCommand { get; private set; }

        public LoginViewModel(IMvxNavigationService navService, IDataAccessService dataAccessService) : base(navService)
        {
            _dataAccessService = dataAccessService;
            LoginCommand = new MvxAsyncCommand(async () => await LoginAsync());
            SignUpCommand = new MvxAsyncCommand(async () => await SignUpAsync());
        }

        private async Task SignUpAsync()
        {
            await _navigationService.Navigate<SignUpViewModel>();
        }

        public async Task LoginAsync()
        {
            await _navigationService.Navigate<SuccessLoginViewModel>();
        }
    }
}
