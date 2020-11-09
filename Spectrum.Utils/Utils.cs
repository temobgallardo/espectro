using System;
using System.Text.RegularExpressions;

namespace Spectrum.Utils
{
    public class Utils
    {
        public static bool IsPasswordValid(string password)
        {
            if (string.IsNullOrEmpty(password)) return false;
            
            string pattern =
                @"^" +              // Matches beginning of a line
                @"(?!.*(.)\1)" +    // For repeated following characters
                @"(?=.*[a-z])" +    // at least a letter
                @"(?=.*[A-Z])" +    // at least a letter
                @"(?=.*[0-9])" +    // at least a digit
                @"([A-Za-z0-9])" +  // Lower and Upper case chars and a digit
                @"{8,15}" +         // Min 8 Max 15 characters
                @"$";               // Matches the end of the line
            return Regex.IsMatch(password, pattern);
        }
        public static bool IsNameValid(string name)
        {
            if (string.IsNullOrEmpty(name)) return false;
            var pattern = "^[a-zA-Z]{2,}";
            return Regex.IsMatch(name, pattern);
        }
        public static bool IsPhoneNumberValid(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber)) return false;
            if (phoneNumber.Length > 10 || phoneNumber.Length < 10) return false;

            string pattern = @"^(\d{3})[ -]?(\d{3})[ -]?(\d{4})";

            return Regex.IsMatch(phoneNumber, pattern);
        }
        public static string FormatPhoneNumber(string phoneNumber)
        {
            if (!IsPhoneNumberValid(phoneNumber)) throw new ArgumentException($"Phone number {phoneNumber} is not valid.");

            string pattern = @"^(\d{3})[ -]?(\d{3})[ -]?(\d{4})";

            return Regex.Replace(phoneNumber, pattern, @"($1)-$2-$3");
        }
        public static bool IsServiceDateCurrent(DateTime date)
        {
            if (date == null) return false;

            if (date < DateTime.Today) return false;

            if (date > DateTime.Today.AddDays(30)) return false;

            return true;
        }
    }
}
