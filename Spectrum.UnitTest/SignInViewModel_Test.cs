using Xunit;
using Moq;
using MvvmCross.Navigation;
using Spectrum.Repository.Abstractions;
using Spectrum.Core.ViewModels;
using Spectrum.Repository.Entities;
using Acr.UserDialogs;
using MvvmCross.Logging;
using System.Threading.Tasks;

namespace Spectrum.UnitTest
{
    public class SignInViewModel_Test
    {
        public Mock<IMvxLogProvider> _logProviderServiceMock;
        public Mock<IMvxNavigationService> _navigationServiceMock;
        public Mock<IDataAccessService<User>> _dataAccessServiceMock;
        public Mock<IUserDialogs> _userDialogsServiceMock;
        public SignInViewModel _signInViewModel;

        public SignInViewModel_Test()
        {
            _logProviderServiceMock = new Mock<IMvxLogProvider>();
            _navigationServiceMock = new Mock<IMvxNavigationService>();
            _dataAccessServiceMock = new Mock<IDataAccessService<User>>();
            _userDialogsServiceMock = new Mock<IUserDialogs>();
        }

        [Fact]
        public async void Should_Login()
        {
            var user = new User() { UserName = "Temo", Password = "12AB123ab" };
            _navigationServiceMock.Setup(nav => nav.Navigate<AccountsViewModel>(null, default)).ReturnsAsync(true);
            _dataAccessServiceMock.Setup(dao => dao.VerifyCredentials(It.IsAny<User>())).Returns(Task.FromResult(true)).Verifiable();

            _signInViewModel = new SignInViewModel(_logProviderServiceMock.Object, _navigationServiceMock.Object, _dataAccessServiceMock.Object, _userDialogsServiceMock.Object);
            _signInViewModel.Password = user.Password; 
            _signInViewModel.UserName = user.UserName;

            var result = await _dataAccessServiceMock.Object.VerifyCredentials(user);
            var result2 = await _navigationServiceMock.Object.Navigate<AccountsViewModel>();
            _signInViewModel.SignInCommand.Execute();
            _dataAccessServiceMock.Verify(dao => dao.VerifyCredentials(user), Times.Once);
            _navigationServiceMock.Verify(nav => nav.Navigate<AccountsViewModel>(null, default), Times.Once);
            Assert.True(_signInViewModel.AreUserAndPassOk);
        }
    }
}
