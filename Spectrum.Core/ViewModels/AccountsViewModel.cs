using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Spectrum.Repository.Abstractions;
using Spectrum.Repository.Entities;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Spectrum.Core.ViewModels
{
    public class AccountsViewModel : BaseViewModel
    {
            private readonly IDataAccessService<User> _dataAccessService;
            private readonly IUserDialogs _userDialogsService;
        // TODO: add a FullName field to displayed in the accounts_row text view

        public MvxObservableCollection<User> Users { get; private set; }
        public bool IsBusy { get; private set; }
        public IMvxAsyncCommand LogoutCommand { get; private set; }

        public AccountsViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IDataAccessService<User> dataAccessService, IUserDialogs userDialogs) : base(logProvider, navigationService)
        {
            _dataAccessService = dataAccessService;
            _userDialogsService = userDialogs;
            Users = new MvxObservableCollection<User>();
            IsBusy = false;
            LogoutCommand = new MvxAsyncCommand(Logout);
        }

        private async Task Logout()
        {
            var config = new ConfirmConfig().SetMessage("Are you sure you want to logout?");
            var result = await _userDialogsService.ConfirmAsync(config);
            
            if (result)
            {
                await NavigationService.Close(this);
                await NavigationService.Navigate<SignInViewModel>();
            }
        }

        public override async void ViewAppearing()
        {
            base.ViewAppearing();
            await ReloadUsers().ConfigureAwait(false);
        }

        private async Task ReloadUsers()
        {
            if (IsBusy)
                return;
            
            try
            {
                IsBusy = true;
                var users = await _dataAccessService.GetEntitiesAsync();
                if (users != null && users.Any())
                {
                    Users.Clear();
                    Users.AddRange(items: users);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
