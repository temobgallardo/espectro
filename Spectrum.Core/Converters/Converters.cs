using MvvmCross.Converters;
using System;
using System.Globalization;

namespace Spectrum.Core.Converters
{
    public class StringToDateTimeValueConverter : MvxValueConverter<DateTime, string>
    {
        protected override DateTime ConvertBack(string value, Type targetType, object parameter, CultureInfo culture)
        {
            return DateTime.TryParse(value, out DateTime currentDate) ? currentDate : DateTime.Now;
        }
    }
}
