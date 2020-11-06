using System;
using Xunit;

namespace Spectrum.UnitTest
{
    public class Util_Test
    {
        [Theory]
        [InlineData("", false)]
        [InlineData("1", false)]
        [InlineData("a", false)]
        [InlineData("1a", false)]
        [InlineData("12345678", false)]
        [InlineData("1234567a", false)]
        [InlineData("abcabc12", false)]
        [InlineData("111222ab", false)]
        [InlineData("abcabc1A", false)]
        [InlineData("111222Ab", false)]
        [InlineData("1234567890abcde", false)]
        [InlineData("abcde1234567890", false)]
        [InlineData("ab123c456d7890", false)]
        [InlineData("1234567890abcde16", false)]
        [InlineData("1234567890654321", false)]
        [InlineData("1234567890_", false)]
        [InlineData("1234567890=", false)]
        [InlineData("Ab123c456d7890", true)]
        [InlineData("1234567890Abcde", true)]
        [InlineData("1234567A", true)]
        public static void IsPasswordValid_Test(string password, bool expected)
        {
            var actual = Utils.Utils.IsPasswordValid(password);
            Assert.Equal(expected, actual);
        }
        [Theory]
        [InlineData("", false)]
        [InlineData("1", false)]
        [InlineData("a", false)]
        [InlineData("1a", false)]
        [InlineData("1234", false)]
        [InlineData("!@#$%^&", false)]
        [InlineData("!", false)]
        [InlineData("@", false)]
        [InlineData("#", false)]
        [InlineData("$", false)]
        [InlineData("%", false)]
        [InlineData("^", false)]
        [InlineData("&", false)]
        [InlineData("Roberto#", false)]
        [InlineData("Chatahuchi", true)]
        [InlineData("Sabana", true)]
        [InlineData("Karl", true)]
        public static void IsNameValid_Test(string name, bool expected)
        {
            var actual = Utils.Utils.IsNameValid(name);
            Assert.Equal(expected, actual);
        }
        [Theory]
        [InlineData("", false)]
        [InlineData("a", false)]
        [InlineData("1", false)]
        [InlineData("123456789a", false)]
        [InlineData("12345678901", false)]
        [InlineData("12a456bb901", false)]
        [InlineData("12#456%b901", false)]
        [InlineData("1234567890", true)]
        public static void IsPhoneNumberValid_Test(string phoneNumber, bool expected)
        {
            var actual = Utils.Utils.IsPhoneNumberValid(phoneNumber);
            Assert.Equal(expected, actual);
        }
        [Theory]
        [InlineData("", "")]
        [InlineData("a", "")]
        [InlineData("1", "")]
        [InlineData("123456789a", "")]
        [InlineData("12345678901", "")]
        [InlineData("12a456bb901", "")]
        [InlineData("12#456%b901", "")]
        [InlineData("1234567890", "(123) 456-7890")]
        public static void FormatPhoneNumber_Test(string phoneNumber, string expected)
        {
            var actual = Utils.Utils.FormatPhoneNumber(phoneNumber);
            Assert.Equal(expected, actual);
        }
        [Fact]
        public static void IsServiceDateCurrent_ShouldBeThisDay()
        {
            DateTime today = DateTime.Today;
            var actual = Utils.Utils.IsServiceDateCurrent(today);
            Assert.True(actual);
        }
        [Fact]
        public static void IsServiceDateCurrent_ShouldNotAllowYesterday()
        { 
            DateTime today = DateTime.Now;
            today.AddDays(-1);
            var actual = Utils.Utils.IsServiceDateCurrent(today);
            Assert.False(actual);
        }
        [Fact]
        public static void IsServiceDateCurrent_ShouldBeWithin30DaysRange()
        {
            DateTime today = DateTime.Today;
            today.AddDays(29);
            var actual = Utils.Utils.IsServiceDateCurrent(today);
            Assert.True(actual);
        }
        [Fact]
        public static void IsServiceDateCurrent_ShouldNotBeOutside30DaysRange()
        {
            DateTime today = DateTime.Today;
            today.AddDays(30);
            var actual = Utils.Utils.IsServiceDateCurrent(today);
            Assert.True(actual);
        }
    }
}
