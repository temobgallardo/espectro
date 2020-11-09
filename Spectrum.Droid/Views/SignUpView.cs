using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Spectrum.Core.ViewModels;

namespace Spectrum.Droid.Views
{
    [MvxActivityPresentation]
    [Activity(Theme = "@style/AppTheme", NoHistory = true)]
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
#pragma warning disable CS0618 // Type or member is obsolete
            datePickerFragment.Show(FragmentManager, "datePicker");
#pragma warning restore CS0618 // Type or member is obsolete
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    ViewModel.BackCommand.Execute();
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }
}