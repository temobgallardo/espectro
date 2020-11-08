using NUnit.Framework;
using Spectrum.UITest.Page;
using Spectrum.UITest.POPBases;
using Xamarin.UITest;

namespace Spectrum.UITest
{
    public class SignInView_Test : BaseTestFixture
    {
        public SignInView_Test(Platform platform) : base(platform)
        {
        }

        [Test]
        public void Should_Login()
        {
            new SignInView_Page()
                .EditUserName("a")
                .EditPassword("a")
                .SignIn();
        }
    }
}
