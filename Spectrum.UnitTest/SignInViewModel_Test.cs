using Xunit;
using Moq;
using MvvmCross.Navigation;
using Spectrum.Repository.Abstractions;
using MvvmCross.ViewModels;
using Spectrum.Core.ViewModels;
using Spectrum.Repository.Entities;
using System;

namespace Spectrum.UnitTest
{
    public class SignInViewModel_Test
    {
        public Mock<IMvxNavigationService> _navigationServiceMock;
        public Mock<IDataAccessService> _dataAccessServiceMock;
        public SignInViewModel _signInViewModel;

        public SignInViewModel_Test()
        {
            _navigationServiceMock = new Mock<IMvxNavigationService>();
            _dataAccessServiceMock = new Mock<IDataAccessService>();
            _signInViewModel = new SignInViewModel(_navigationServiceMock.Object, _dataAccessServiceMock.Object);
        }

        [Fact]
        public void Should_EnableSignUpButton()
        {
            _navigationServiceMock.Setup(nav => nav.Navigate<SuccessLoginViewModel>(null, default)).ReturnsAsync(true);
            _signInViewModel.User = new User { FirstName = "", LastName = "", Password = "a", Start = DateTime.Today, UserName = "a" };
            _signInViewModel.SignInCommand.Execute();

            _navigationServiceMock.Verify(nav => nav.Navigate<SuccessLoginViewModel>(null, default), Times.Once);
            Assert.True(_signInViewModel.IsUserOrPassOk);
        }
    }
}
