using Spectrum.UITest.POPBases;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace Spectrum.UITest.Page
{
    public class SignUpView_Page : BasePage
    {
        public Query ETFirstName = x => x.Marked("et_first_name");
        public Query ETLastName = x => x.Marked("et_last_name");
        public Query ETUserName = x => x.Marked("et_user_name");
        public Query ETPhoneNumbmer = x => x.Marked("et_phone_number");
        public Query ETPassword = x => x.Marked("et_password");
        public Query TVServiceDate = x => x.Marked("tv_dp_service_start");
        public Query BtnServiceOk = x => x.Marked("OK");
        public Query BtnServiceCancel = x => x.Marked("CANCEL");
        public Query DPServiceDatePicker = x => x.Marked("datePicker");
        public Query BtnSignUp = x => x.Marked("btn_create_account");
        public Query Current = x => x.Marked("Sign up");

        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = x => x.Marked("Sign up")
        };
        public SignUpView_Page EditFirstName(string firstName)
        {
            app.WaitForElement(Current);
            app.WaitForElement(ETFirstName);
            app.ClearText(ETFirstName);
            app.EnterText(firstName);
            return this;
        }
        public SignUpView_Page EditLastName(string value)
        {
            app.WaitForElement(Current);
            app.WaitForElement(ETLastName);
            app.ClearText(ETLastName);
            app.EnterText(value);
            return this;
        }
        public SignUpView_Page EditPhoneNumber(string value)
        {
            app.WaitForElement(Current);
            app.WaitForElement(ETPhoneNumbmer);
            app.ClearText(ETPhoneNumbmer);
            app.EnterText(value);
            return this;
        }
        public SignUpView_Page EditUserName(string userName)
        {
            app.WaitForElement(Current);
            app.WaitForElement(ETUserName);
            app.ClearText(ETUserName);
            app.EnterText(userName);
            return this;
        }
        public SignUpView_Page EditPassword(string pass)
        {
            app.WaitForElement(Current);
            app.WaitForElement(ETPassword);
            app.ClearText(ETPassword);
            app.EnterText(pass);
            return this;
        }
        public SignUpView_Page EditServiceDate()
        {
            app.WaitForElement(Current);
            app.WaitForElement(TVServiceDate);
            app.Tap(TVServiceDate);
            app.WaitForElement(DPServiceDatePicker);
            app.Tap(BtnServiceOk);
            return this;
        }
        public void SignUp()
        {
            app.WaitForElement(Current);
            app.WaitForElement(BtnSignUp);
            app.Tap(BtnSignUp);
        }
    }
}
