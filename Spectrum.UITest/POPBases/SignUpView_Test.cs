using NUnit.Framework;
using Spectrum.UITest.Page;
using Xamarin.UITest;

namespace Spectrum.UITest.POPBases
{
    class SignUpView_Test : BaseTestFixture
    {
        public SignUpView_Test(Platform platform) : base(platform)
        {
        }

        [Test]
        public void Should_SignUp()
        {
            var userName = "Temo";
            var password = "12AB123ab";

            new SignInView_Page()
                .SignUp();

            new SignUpView_Page()
                .EditFirstName(userName)
                .EditLastName("Banos")
                .EditPhoneNumber("1234567890")
                .EditUserName(userName)
                .EditPassword(password)
                .SignUp();

            new SuccessSignupView_Page()
                .Login();
        }
        [Test]
        public void Should_SignUpThenSignIn()
        {
            new SignInView_Page()
                .SignUp();

            var userName = "Temo";
            var password = "12AB123ab";
            new SignUpView_Page()
                .EditFirstName(userName)
                .EditLastName("Banos")
                .EditPhoneNumber("1234567890")
                .EditUserName(userName)
                .EditPassword(password)
                .SignUp();

            new SuccessSignupView_Page()
                .Login();

            new SignInView_Page()
                .EditUserName(userName)
                .EditPassword(password)
                .SignIn();
        }
    }
}
