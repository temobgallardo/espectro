using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Threading.Tasks;

namespace Spectrum.Core.ViewModels
{
    public class SuccessSignupViewModel : BaseViewModel
    {
        public IMvxAsyncCommand NavigateToLoginCommand { get; private set; }
        public IMvxAsyncCommand BackCommand { get; private set; }

        public SuccessSignupViewModel(IMvxNavigationService navigationService) : base(navigationService)
        {
            NavigateToLoginCommand = new MvxAsyncCommand(Back);
            BackCommand = new MvxAsyncCommand(Back);
        }

        private async Task Back()
        {
            await _navigationService.Close(this);
            await _navigationService.Navigate<SignInViewModel>();
        }
    }
}
