using Xunit;
using Moq;
using MvvmCross.Navigation;
using Spectrum.Repository.Abstractions;
using Spectrum.Core.ViewModels;
using Spectrum.Repository.Entities;
using Acr.UserDialogs;

namespace Spectrum.UnitTest
{
    public class SignInViewModel_Test
    {
        public Mock<IMvxNavigationService> _navigationServiceMock;
        public Mock<IDataAccessService<User>> _dataAccessServiceMock;
        public Mock<IUserDialogs> _userDialogsServiceMock;
        public SignInViewModel _signInViewModel;

        public SignInViewModel_Test()
        {
            _navigationServiceMock = new Mock<IMvxNavigationService>();
            _dataAccessServiceMock = new Mock<IDataAccessService<User>>();
            _userDialogsServiceMock = new Mock<IUserDialogs>();
            _signInViewModel = new SignInViewModel(_navigationServiceMock.Object, _dataAccessServiceMock.Object, _userDialogsServiceMock.Object);
        }

        [Fact]
        public void Should_EnableSignUpButton()
        {
            _navigationServiceMock.Setup(nav => nav.Navigate<SuccessLoginViewModel>(null, default)).ReturnsAsync(true);
            _signInViewModel.Password = "a"; 
            _signInViewModel.UserName = "a" ;
            _signInViewModel.SignInCommand.Execute();
            _navigationServiceMock.Verify(nav => nav.Navigate<SuccessLoginViewModel>(null, default), Times.Once);
            Assert.True(_signInViewModel.IsUserOrPassOk);
        }
    }
}
