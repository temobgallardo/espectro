using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using System.Threading.Tasks;

namespace Spectrum.Core.ViewModels
{
    public class SuccessSignupViewModel : BaseViewModel
    {
        public IMvxAsyncCommand NavigateToLoginCommand { get; private set; }

        public SuccessSignupViewModel(IMvxLogProvider loggerProvider, IMvxNavigationService navigationService) : base(loggerProvider, navigationService)
        {
            NavigateToLoginCommand = new MvxAsyncCommand(OnBackCommand);
        }

        protected override async Task OnBackCommand()
        {
            await NavigationService.Close(this);
            await NavigationService.Navigate<SignInViewModel>();
        }
    }
}
