using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using Spectrum.Repository.Abstractions;
using Spectrum.Repository.Entities;
using System;
using System.Threading.Tasks;

namespace Spectrum.Core.ViewModels
{
    public class SignInViewModel : BaseViewModel
    {
        private readonly IDataAccessService<User> _dataAccessService;
        private readonly IUserDialogs _userDialogsService;
        private bool _isUserOrPassOk;
        private string _userName;
        private string _password;

        public bool AreUserAndPassOk
        {
            get => _isUserOrPassOk;
            set => SetProperty(ref _isUserOrPassOk, value);
        }
        public string UserName
        {
            get => _userName;
            set
            {
                SetProperty(ref _userName, value);

                AreUserAndPassOk = Utils.Utils.IsNameValid(_userName) && Utils.Utils.IsPasswordValid(_password);
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value);

                AreUserAndPassOk = Utils.Utils.IsNameValid(_userName) && Utils.Utils.IsPasswordValid(_password);

                RaiseErrorInteractionOnPasswordIssue(Utils.Utils.IsPasswordValid(_password));
            }
        }
        public IMvxAsyncCommand SignInCommand { get; private set; }
        public IMvxAsyncCommand SignUpCommand { get; private set; }

        public SignInViewModel(IMvxLogProvider logProvider, IMvxNavigationService navService, IDataAccessService<User> dataAccessService, IUserDialogs userDialogs) : base(logProvider, navService)
        {
            AreUserAndPassOk = false;
            _userDialogsService = userDialogs;
            _dataAccessService = dataAccessService;
            SignInCommand = new MvxAsyncCommand(SignInAsync);
            SignUpCommand = new MvxAsyncCommand(SignUpAsync);
        }

        public void RaiseErrorInteractionOnPasswordIssue(bool ok)
        {
            if (ok)
            {
                ErrorInteraction.Raise(null);
                return;
            }

            ErrorInteraction.Raise("Password must contain: \nFrom 8 - 15 characters.\nAt least one letter and digit.\nFollowing characters must be different.");
        }

        private async Task SignUpAsync()
        {
            await NavigationService.Navigate<SignUpViewModel>();
        }

        public async Task SignInAsync()
        {
            _userDialogsService.ShowLoading("Loggin...");

            var user = new User() { UserName = _userName, Password = _password };
            var isSigned = await _dataAccessService.VerifyCredentials(user);
            if (!isSigned)
            {
                _userDialogsService.Alert("Your user is incorrect. Try again or log in with a different account.");

                return;
            }

            _userDialogsService.HideLoading();

            var toastConfig = new ToastConfig("Successfully Logged!")
                .SetPosition(ToastPosition.Top);
            _userDialogsService.Toast(toastConfig);

            await Task.Delay(TimeSpan.FromSeconds(2));

            await NavigationService.Navigate<AccountsViewModel>();
        }

        protected override async Task OnBackCommand()
        {
            await NavigationService.Close(this);
        }
    }
}
