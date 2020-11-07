using Android.App;
using Android.OS;
using Android.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Spectrum.Core.ViewModels;

namespace Spectrum.Droid.Views
{
    [MvxActivityPresentation]
    [Activity(Theme = "@style/AppTheme")]
    public class SignUpView : MvxAppCompatActivity<SignUpViewModel>
    {
        private Button _button;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_sign_up);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);
            SupportActionBar.Title = ApplicationContext.Resources.GetString(Resource.String.add_user_title);
            _button = FindViewById<Button>(Resource.Id.dp_service_start);
            _button.Click += delegate
            {
                ShowDatePickerDialog();
            };
        }

        protected void ShowDatePickerDialog()
        {
            var datePickerFragment = new DatePickerFragment();
            datePickerFragment.Show(FragmentManager, "datePicker");
        }
    }
}