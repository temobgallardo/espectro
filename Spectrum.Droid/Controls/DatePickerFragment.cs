using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Widget;
using System;

namespace Spectrum.Droid.Controls
{
#pragma warning disable CS0618 // Type or member is obsolete
    public class DatePickerFragment : DialogFragment, DatePickerDialog.IOnDateSetListener
#pragma warning restore CS0618 // Type or member is obsolete
    {
        public static readonly string TAG = "Spectrum:" + typeof(DatePickerFragment).Name.ToUpper();
        public Action<DateTime> DateSelectedHandler { get; set; } = delegate { };

        public DatePickerFragment(Action<DateTime> action)
        {
            DateSelectedHandler = action;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            return new DatePickerDialog(Context, this, DateTime.Today.Year, DateTime.Today.Month - 1, DateTime.Today.Day);
        }

        public void OnDateSet(DatePicker view, int year, int month, int dayOfMonth)
        {
            Log.Debug(TAG, new DateTime(year, month + 1, dayOfMonth).ToLongDateString());
            DateSelectedHandler(new DateTime(year, month + 1, dayOfMonth));
        }
    }
}