using Android.App;
using Android.OS;
using Android.Util;
using Android.Widget;
using System;
using Xamarin.Forms;

namespace Spectrum.Droid.Views
{
#pragma warning disable CS0618 // Type or member is obsolete
    public class DatePickerFragment : DialogFragment, Android.App.DatePickerDialog.IOnDateSetListener
#pragma warning restore CS0618 // Type or member is obsolete
    {
        public static readonly string TAG = "Spectrum:" + typeof(DatePickerFragment).Name.ToUpper();
        readonly Action<DateTime> _dateSelectedHandler = delegate { };

        public DatePickerFragment(Action<DateTime> action)
        {
            _dateSelectedHandler = action;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            return new DatePickerDialog(Context, this, DateTime.Today.Year, DateTime.Today.Month - 1, DateTime.Today.Day);
        }

        public void OnDateSet(Android.Widget.DatePicker view, int year, int month, int dayOfMonth)
        {
            var selectedDate = new DateTime(year, month + 1, dayOfMonth);
            Log.Debug(TAG, selectedDate.ToLongDateString());
            _dateSelectedHandler(selectedDate);
        }
    }
}