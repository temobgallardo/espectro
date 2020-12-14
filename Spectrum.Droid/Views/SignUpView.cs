using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Spectrum.Core.ViewModels;
using Spectrum.Droid.Controls;
using System;

namespace Spectrum.Droid.Views
{
    [MvxActivityPresentation]
    [Activity(Theme = "@style/AppTheme", NoHistory = true)]
    public class SignUpView : BaseView<SignUpViewModel>
    {
        private TextView _tvDatePicker;
        private DatePickerFragment _datePickerFragment;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_sign_up);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);
            SupportActionBar.Title = ApplicationContext.Resources.GetString(Resource.String.add_user_title);
            _tvDatePicker = FindViewById<TextView>(Resource.Id.tv_dp_service_start);
            _datePickerFragment = new DatePickerFragment(delegate (DateTime time)
            {
                _tvDatePicker.Text = time.ToLongDateString();
            });
            _tvDatePicker.Click += DateSelected_OnClick;
        }

        void DateSelected_OnClick(object sender, EventArgs args)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            _datePickerFragment.Show(FragmentManager, "datePicker");
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