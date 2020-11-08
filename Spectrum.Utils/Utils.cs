using System;
using System.Text.RegularExpressions;

namespace Spectrum.Utils
{
    public class Utils
    {
        public static bool IsPasswordValid(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return false;
            }

            string expression =
                @"^" +              // Matches beginning of a line
                @"(?!.*(.)\1)" +    // For repeated following characters
                @"(?=.*[A-Za-z])" + // at least a letter
                @"(?=.*[0-9])" +    // at least a digit
                @"([A-Za-z0-9])" +  // Lower and Upper case chars and a digit
                @"{8,15}" +         // Min 8 Max 15 characters
                @"$";               // Matches the end of the line
            return Regex.IsMatch(password, expression);
        }
        public static bool IsNameValid(string name)
        {
            return true;
        }
        public static bool IsPhoneNumberValid(string phoneNumber)
        {
            throw new NotImplementedException();
        }
        public static string FormatPhoneNumber(string phoneNumber)
        {
            throw new NotImplementedException();
        }
        public static bool IsServiceDateCurrent(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
