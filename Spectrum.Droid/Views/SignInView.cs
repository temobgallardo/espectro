using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using MvvmCross.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.ViewModels;
using Spectrum.Core.ViewModels;

namespace Spectrum.Droid.Views
{
    [MvxActivityPresentation]
    [Activity(Theme = "@style/AppTheme", LaunchMode = Android.Content.PM.LaunchMode.SingleTask)]
    public class SignInView : BaseView<SignInViewModel>
    {
        private TextInputLayout _passwordInputLayout;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_sign_in);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);
            SupportActionBar.Title = ApplicationContext.Resources.GetString(Resource.String.signin_title);

            ErrorInteraction = new MvxInteraction<string>();
            ErrorInteraction.Requested += OnErrorInteraction;

            _passwordInputLayout = FindViewById<TextInputLayout>(Resource.Id.til_et_password);

            CreateBindings();
        }

        protected override void OnErrorInteraction(object sender, MvxValueEventArgs<string> e)
        {
            // As per documentation the property ErrorEnabled will be enabled automatically if Error is not null or empty.
            _passwordInputLayout.Error = e.Value;
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
            var set = this.CreateBindingSet<SignInView, SignInViewModel>();
            set.Bind(this).For(v => v.ErrorInteraction).To(vm => vm.ErrorInteraction);
            set.Apply();
        }
    }
}