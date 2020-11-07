using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using System;

namespace Spectrum.Droid.Views
{
    public class DatePickerFragment : DialogFragment, DatePickerDialog.IOnDateSetListener
    {
        public DateTime SelectedDate { get; set; }

        //protected DatePickerFragment(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        //{
        //    SelectedDate = new DateTime();
        //}

        public DatePickerFragment()
        {
            SelectedDate = new DateTime();
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            return new DatePickerDialog(Context, this, DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
        }

        public void OnDateSet(DatePicker view, int year, int month, int dayOfMonth)
        {
            SelectedDate = new DateTime(year, month, dayOfMonth);
        }
    }
}