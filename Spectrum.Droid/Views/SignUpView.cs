using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using MvvmCross.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.ViewModels;
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

            ErrorInteraction = new MvxInteraction<string>();
            ErrorInteraction.Requested += OnErrorInteraction;

            _tvDatePicker = FindViewById<TextView>(Resource.Id.tv_dp_service_start);
            _datePickerFragment = new DatePickerFragment(SetDateTime);
            _tvDatePicker.Click += DateSelected_OnClick;

            CreateBindings();
        }

        void DateSelected_OnClick(object sender, EventArgs args)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            _datePickerFragment.Show(FragmentManager, "datePicker");
#pragma warning restore CS0618 // Type or member is obsolete  
        }

        private void SetDateTime(DateTime date)
        {
            _tvDatePicker.Text = date.ToLongDateString();
            ViewModel.ServiceDate = date;
        }

        protected override void OnErrorInteraction(object sender, MvxValueEventArgs<string> e)
        {
            var fn = FindViewById<TextInputLayout>(Resource.Id.til_first_name);
            var ln = FindViewById<TextInputLayout>(Resource.Id.til_last_name);
            var un = FindViewById<TextInputLayout>(Resource.Id.til_user_name);
            var p = FindViewById<TextInputLayout>(Resource.Id.til_password);
            var pn = FindViewById<TextInputLayout>(Resource.Id.til_phone_number);
            var ss = FindViewById<TextInputLayout>(Resource.Id.til_service_start);

            if (string.IsNullOrEmpty(e.Value))
            {
                fn.Error = e.Value;
                ln.Error = e.Value;
                un.Error = e.Value;
                p.Error = e.Value;
                pn.Error = e.Value;
                ss.Error = e.Value;
                return;
            }

            // As per documentation the property ErrorEnabled will be enabled automatically if Error is not null or empty.
            if (e.Value.Contains("First Name"))
            {
                fn.Error = e.Value;
            }
            if (e.Value.Contains("Last Name"))
            {
                ln.Error = e.Value;
            }
            if (e.Value.Contains("User Name"))
            {
                un.Error = e.Value;
            }
            if (e.Value.Contains("Password"))
            {
                p.Error = e.Value;
            }
            if (e.Value.Contains("Phone Number"))
            {
                pn.Error = e.Value;
            }
            if (e.Value.Contains("Service Date"))
            {
                ss.Error = e.Value;
            }
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

        protected override void CreateBindings()
        {
            var set = this.CreateBindingSet<SignUpView, SignUpViewModel>();
            set.Bind(this).For(v => v.ErrorInteraction).To(vm => vm.ErrorInteraction);
            set.Apply();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (_datePickerFragment != null)
            {
                _datePickerFragment.DateSelectedHandler -= SetDateTime;
            }

            if (_tvDatePicker != null)
            {
                _tvDatePicker.Click -= DateSelected_OnClick;
            }
        }
    }
}