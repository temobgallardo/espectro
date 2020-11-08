using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Spectrum.Repository.Abstractions;
using Spectrum.Repository.Entities;
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

        public bool IsUserOrPassOk
        {
            get => _isUserOrPassOk;
            set => SetProperty(ref _isUserOrPassOk, value);
        }
        public string UserName
        {
            get => _userName;
            set
            {
                IsUserOrPassOk = Utils.Utils.IsNameValid(_userName) && Utils.Utils.IsPasswordValid(_password);

                SetProperty(ref _userName, value);
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                IsUserOrPassOk = Utils.Utils.IsNameValid(_userName) && Utils.Utils.IsPasswordValid(_password);

                SetProperty(ref _password, value);
            }
        }
        public IMvxAsyncCommand SignInCommand { get; private set; }
        public IMvxAsyncCommand SignUpCommand { get; private set; }
        public IMvxAsyncCommand BackCommand { get; private set; }

        public SignInViewModel(IMvxNavigationService navService, IDataAccessService<User> dataAccessService, IUserDialogs userDialogs) : base(navService)
        {
            IsUserOrPassOk = false;
            _userDialogsService = userDialogs;
            _dataAccessService = dataAccessService;
            SignInCommand = new MvxAsyncCommand(SignInAsync);
            SignUpCommand = new MvxAsyncCommand(SignUpAsync);
            BackCommand = new MvxAsyncCommand(Back);
        }

        public void AlertOnUserNameIssue(bool ok)
        {
            if (ok) return;

            _userDialogsService.Alert("User name must: \n Be not empty.");
        }

        public void AlertOnPasswordIssue(bool ok)
        {
            if (ok) return;

            _userDialogsService.Alert("Password must contain: \nFrom 8 - 15 characters.\nAt least one words and digit.\nFollowing characters must be different.");
        }

        private async Task SignUpAsync()
        {
            await _navigationService.Navigate<SignUpViewModel>();
        }

        public async Task SignInAsync()
        {
            //AlertOnPasswordIssue(Utils.Utils.IsPasswordValid(Password));
            //AlertOnUserNameIssue(Utils.Utils.IsNameValid(UserName));
            var user = new User() { UserName = _userName, Password = _password };
            var isSigned = await _dataAccessService.VerifyCredentials(user);
            if (!isSigned)
            {
                _userDialogsService.Alert("Your user is incorrect. Try again or log with different credentials");
                
                return;
            }

            await _navigationService.Navigate<SuccessLoginViewModel>();
        }

        private async Task Back()
        {
            await _navigationService.Close(this);
        }
    }
}
