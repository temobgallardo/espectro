using Spectrum.UITest.POPBases;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace Spectrum.UITest.Page
{
    public class SignInView_Page : BasePage
    {
        public Query ETUserName = x => x.Marked("et_user_name");
        public Query ETPassword = x => x.Marked("et_password");
        public Query BtnSignIn = x => x.Marked("btn_signin");
        public Query TVSignUp = x => x.Marked("tv_sign_up");
        public Query Current = x => x.Marked("Sign in");

        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = x => x.Marked("Sign in")
        };

        public SignInView_Page EditUserName(string userName)
        {
            app.WaitForElement(Current);
            app.WaitForElement(ETUserName);
            app.ClearText(ETUserName);
            app.EnterText(userName);
            return this;
        }
        public SignInView_Page EditPassword(string pass)
        {
            app.WaitForElement(Current);
            app.WaitForElement(ETPassword);
            app.ClearText(ETPassword);
            app.EnterText(pass);
            return this;
        }
        public void SignIn()
        {
            app.WaitForElement(Current);
            app.WaitForElement(BtnSignIn);
            app.Tap(BtnSignIn);
        }
    }
}
