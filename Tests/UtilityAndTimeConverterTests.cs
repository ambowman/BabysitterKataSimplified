using BabySitterKataSimple;
using Shouldly;
using Xunit;


namespace Tests
{
    public class UtilityAndTimeConverterTests
    {
        Utility utility = new Utility();
        TimeConverter timeConverter = new TimeConverter();

        public void MakeStringIntoArray()
        {
            string timeString = "5:00AM";
            var result = utility.MakeCharArray(timeString);
            result[0].ShouldBeOfType<char>();
            result[5].ShouldBeOfType<char>();
            result[0].ShouldBe('5');
        }
        [Fact]
        public void CheckAmOrPmWhenGivenATimeWithPm()
        {
            string timeString = "5:00PM";
            var result = utility.CheckAmOrPm(timeString);
            result.ShouldBe(true);
        }
        [Fact]
        public void CheckAmOrPmWhenGivenATimeWithAm()
        {
            string timeString = "5:00AM";
            var result = utility.CheckAmOrPm(timeString);
            result.ShouldBe(false);
        }
        [Fact]
        public void RemoveAmFromCharArray()
        {
            string timeString = "5:00AM";
            var timeArray = utility.MakeCharArray(timeString);
            var result = utility.RemoveAmPm(timeArray);
            result.ShouldBe("5:00");
        }
        [Fact]
        public void RemovePmFromCharArray()
        {
            string timeString = "5:00PM";
            var timeArray = utility.MakeCharArray(timeString);
            var result = utility.RemoveAmPm(timeArray);
            result.ShouldBe("5:00");
        }
        [Fact]
        public void RemoveColonFromCharArray()
        {
            string timeString = "5:00";
            var timeArray = utility.MakeCharArray(timeString);
            var result = utility.RemoveColon(timeArray);
            result.ShouldBe("5.00");
        }
        [Fact]
        public void ConvertTimeArrayToDouble()
        {
            string timeString = "5.00";
            var timeArray = utility.MakeCharArray(timeString);
            var result = timeConverter.ConvertTimeArrayToDouble(timeArray);
            result.ShouldBe(5.00);
        }
        [Fact]
        public void MakeMilitaryTimeMid()
        {
            bool isPm = false;
            var result = timeConverter.MakeMilitaryTime(12.00, isPm);
            result.ShouldBe(24.00);
        }
        [Fact]
        public void MakeMilitaryTimeNoon()
        {
            bool isPm = true;
            var result = timeConverter.MakeMilitaryTime(12.00, isPm);
            result.ShouldBe(12.00);
        }
        [Fact]
        public void MakeMilitaryTimeAm()
        {
            bool isPm = false;
            var result = timeConverter.MakeMilitaryTime(11.00, isPm);
            result.ShouldBe(11.00);
        }
        [Fact]
        public void MakeMilitaryTimePm()
        {
            bool isPm = true;
            var result = timeConverter.MakeMilitaryTime(1.00, isPm);
            result.ShouldBe(13.00);
        }
        [Fact]
        public void ConvertTimeString()
        {
            var result = timeConverter.ConvertTimeString("5:00PM");
            result.ShouldBe(17.00);
        }
    }
}
