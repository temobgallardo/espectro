using Android.OS;
using Android.Widget;
using MvvmCross.Base;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.ViewModels;

namespace Spectrum.Droid.Views
{
    public class BaseView<T> : MvxAppCompatActivity<T> where T: MvxViewModel
    {
        private IMvxInteraction<string> _errorInteraction;
        public IMvxInteraction<string> ErrorInteraction
        {
            get => _errorInteraction;

            set
            {
                if (_errorInteraction != null)
                {
                    _errorInteraction.Requested -= OnErrorInteraction;
                }

                _errorInteraction = value;
                _errorInteraction.Requested += OnErrorInteraction;
            }
        }

        protected virtual void OnErrorInteraction(object sender, MvxValueEventArgs<string> e)
        {
            var toast = Toast.MakeText(ApplicationContext, e.Value, ToastLength.Long);
            toast.Show();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        protected virtual void CreateBindings() { }
    }
}