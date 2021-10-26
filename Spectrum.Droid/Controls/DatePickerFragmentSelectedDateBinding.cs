using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target;
using MvvmCross.Platforms.Android.WeakSubscription;
using System;
using System.Reflection;

namespace Spectrum.Droid.Controls
{
    public class DatePickerFragmentSelectedDateBinding : MvxPropertyInfoTargetBinding<DatePickerFragment>
    {
        private bool _subscribed;
        private IDisposable _subscription;
        private string TAG = "Spectrum:" + typeof(DatePickerFragmentSelectedDateBinding).Name.ToUpper();

        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWayToSource;

        public DatePickerFragmentSelectedDateBinding(object target, PropertyInfo targetPropertyInfo)
            : base(target, targetPropertyInfo)
        { 
        
        }

        // this variable isn't used, but including this here prevents Mono from optimising the call out!
        private DateTime JustForReflection
        {
            get { return View.SelectedDate; }
            set { View.SelectedDate = value; }
        }

        //protected override void SetValueImpl(object target, object value)
        //{
        //    var view = target as DatePickerFragment;
        //    if (view == null) return;

        //    view.SelectedDate = (DateTime)value;
        //}

        //private void DatePickerSelectedDateChanged(object sender, DatePickerFragment.SelectedDateChangedEventArgs e)
        //{
        //    var value = e.SelectedDate;
        //    MvxBindingLog.Trace(TAG, $"The target.SelectedDate has changed to {value}!");
        //    FireValueChanged(value);
        //}

        //public override void SubscribeToEvents()
        //{
        //    var datePicker = View;
        //    if (datePicker == null)
        //    {
        //        MvxBindingLog.Trace(TAG, "Error - DatePickerFragment is null in DatePickerFragmentBinding");
        //        return;
        //    }

        //    //_subscribed = true;
        //    //datePicker.SelectedDateChanged += TargetOnSelectedDateChanged;
        //    MvxBindingLog.Trace(TAG, "Subscribing the bindings!");
        //    _subscription = datePicker.WeakSubscribe<DatePickerFragment, DatePickerFragment.SelectedDateChangedEventArgs>(nameof(datePicker.SelectedDateChanged), DatePickerSelectedDateChanged);
        //}

        private void TargetOnSelectedDateChanged(object sender, EventArgs e)
        {
            var target = View;
            if (target == null) return;

            var value = target.SelectedDate;
            MvxBindingLog.Trace(TAG, $"The target.SelectedDate has changed to {value}!");
            FireValueChanged(value);
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            //if (isDisposing)
            //{
            //    var myView = View;
            //    if (myView != null && _subscribed)
            //    {
            //        myView.SelectedDateChanged -= TargetOnSelectedDateChanged;
            //        _subscribed = false;
            //    }
            //}

            if (isDisposing)
            {
                _subscription?.Dispose();
                _subscription = null;
            }
        }
    }
}