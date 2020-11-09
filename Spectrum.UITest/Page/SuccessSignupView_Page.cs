using Spectrum.UITest.POPBases;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace Spectrum.UITest.Page
{
    class SuccessSignupView_Page : BasePage
    {
        public Query BtnSignIn = x => x.Marked("btn_login");
        public Query Current = x => x.Marked("Success!");

        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = x => x.Marked("Success!")
        };
        public void Login()
        {
            app.WaitForElement(Current);
            app.WaitForElement(BtnSignIn);
            app.Tap(BtnSignIn);
        }
    }
}
