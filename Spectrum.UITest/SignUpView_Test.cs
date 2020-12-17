using NUnit.Framework;
using Spectrum.UITest.Page;
using Spectrum.UITest.POPBases;
using Xamarin.UITest;

namespace Spectrum.UITest
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
                .EditServiceDate()
                .SignUp();

            new SuccessSignupView_Page()
                .Login();
        }
        [Test]
        public void Should_SignUpThenSignIn()
        {
            new SignInView_Page()
                .SignUp();

            var userName = "Temo2";
            var password = "12AB123ab";
            new SignUpView_Page()
                .EditFirstName(userName)
                .EditLastName("Banos2")
                .EditPhoneNumber("1234567892")
                .EditUserName(userName)
                .EditPassword(password)
                .EditServiceDate()
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
