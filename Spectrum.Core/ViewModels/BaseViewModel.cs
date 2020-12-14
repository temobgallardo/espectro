using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace Spectrum.Core.ViewModels
{
    public class BaseViewModel : MvxNavigationViewModel
    {
        private MvxInteraction<string> _errorInteraction;
        public MvxInteraction<string> ErrorInteraction
        {
            get
            {
                if (_errorInteraction == null)
                {
                    _errorInteraction = new MvxInteraction<string>();
                }
                
                return _errorInteraction;
            }
            private set => _errorInteraction = value;
        }

        public IMvxAsyncCommand BackCommand { get; private set; }

        public BaseViewModel(IMvxLogProvider logProvider,
            IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            BackCommand = new MvxAsyncCommand(OnBackCommand);
        }

        /// <summary>
        /// for custom back implementation
        /// </summary>
        /// <returns></returns>
        protected virtual async Task OnBackCommand()
        {
            await NavigationService.Close(this);
        }
    }
}
