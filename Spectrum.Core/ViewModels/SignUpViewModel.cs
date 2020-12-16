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
                SetProperty(ref _firstName, value);
                AreFieldsPopulated = AreFieldsRight();
                var isOk = Utils.Utils.IsNameValid(FirstName);
                // TODO: Move this hard-coded message to a config file
                RaiseErrorInteraction(isOk, "First Name must be alphanumeric.");
            }
        }
        public string LastName
        {
            get => _lastName;
            set
            {
                SetProperty(ref _lastName, value);
                var isOk = Utils.Utils.IsNameValid(LastName);
                AreFieldsPopulated = AreFieldsRight();
                // TODO: Move this hard-coded message to a config file
                RaiseErrorInteraction(isOk, "Last Name must be alphanumeric.");
            }
        }
        public string UserName
        {
            get => _userName;
            set
            {
                SetProperty(ref _userName, value);
                var isOk = Utils.Utils.IsNameValid(UserName);
                AreFieldsPopulated = AreFieldsRight();
                // TODO: Move this hard-coded message to a config file
                RaiseErrorInteraction(isOk, "User Name must be alphanumeric.");
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value);
                var isOk = Utils.Utils.IsPasswordValid(Password);
                AreFieldsPopulated = AreFieldsRight();
                // TODO: Move this hard-coded message to a config file
                RaiseErrorInteraction(isOk, "Password must contain: From 8 - 15 characters. At least one letter and digit. Following characters must be different.");
            }
        }
        // Todo: First time phone number is typed will be erased, an if playing with it will do the same. It should be formated.
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                value = Utils.Utils.GetNumbersFromFormatedPhoneNumber(value);

                if (Utils.Utils.IsPhoneNumberValid(value) && !_isPhoneNumberAlreadyFormatted)
                {
                    _phoneNumberNotFormated = value;
                    _isPhoneNumberAlreadyFormatted = true;
                }

                if (Utils.Utils.IsPhoneNumberValid(value))
                {
                    value = Utils.Utils.FormatPhoneNumber(value);
                }
                else
                {
                    _isPhoneNumberAlreadyFormatted = false;
                }

                SetProperty(ref _phoneNumber, value);

                var isOk = _isPhoneNumberAlreadyFormatted || Utils.Utils.IsPhoneNumberValid(PhoneNumber);
                AreFieldsPopulated = AreFieldsRight();
                // TODO: Move this hard-coded message to a config file
                RaiseErrorInteraction(isOk, "Phone Number must be numeric and have 10 digits.");
            }
        }
        public DateTime ServiceDate
        {
            get => _serviceDate;
            set { 
                SetProperty(ref _serviceDate, value);

                var isOk = Utils.Utils.IsServiceDateCurrent(_serviceDate);
                AreFieldsPopulated = AreFieldsRight();
                // TODO: Move this hard-coded message to a config file
                RaiseErrorInteraction(isOk, $"Service Date must be between {DateTime.Now} and {DateTime.Now.AddDays(30)}.");
            }
        }
        public IMvxAsyncCommand SignUpCommand { get; private set; }

        public SignUpViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IDataAccessService<User> dataAccessService, IUserDialogs userDialogs) : base(logProvider, navigationService)
        {
            AreFieldsPopulated = true;
            _isPhoneNumberAlreadyFormatted = false;
            _userDialogsService = userDialogs;
            _dataAccessService = dataAccessService;
            SignUpCommand = new MvxAsyncCommand(SignUpAsync);
        }

        private bool AreFieldsRight()
        {
            try
            {
                var isPhoneOk = _isPhoneNumberAlreadyFormatted || Utils.Utils.IsPhoneNumberValid(PhoneNumber);

                if (Utils.Utils.IsNameValid(FirstName)
                    && Utils.Utils.IsNameValid(LastName)
                    && Utils.Utils.IsNameValid(UserName)
                    && Utils.Utils.IsPasswordValid(Password)
                    && Utils.Utils.IsServiceDateCurrent(ServiceDate)
                    && isPhoneOk)
                    return true;
            }
            catch (ArgumentException)
            { }

            return false;
        }

        public void RaiseErrorInteraction(bool ok, string message)
        {
            if (ok)
            {
                ErrorInteraction.Raise(null);
                return;
            }

            ErrorInteraction.Raise(message);
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

            var alreadyRegistered = await _dataAccessService.AlreadyRegistered(user);

            if (alreadyRegistered)
            {
                // TODO: Move this hard-coded message to a config file
                _userDialogsService.Alert("This user has already Sign up, try with a different user, first and last name, and phone.");

                return;
            }

            var affected = await _dataAccessService.SaveEntityAsync(user);

            if (affected == 0)
            {
                // TODO: Move this hard-coded message to a config file
                _userDialogsService.Alert("One or more fields are incorrect.");

                return;
            }

            await NavigationService.Navigate<SuccessSignupViewModel>();
        }

        protected override async Task OnBackCommand()
        {
            await NavigationService.Close(this);
            await NavigationService.Navigate<SignInViewModel>();
        }
    }
}
