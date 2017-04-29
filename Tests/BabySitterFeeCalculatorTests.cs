using BabySitterKataSimple;
using Shouldly;
using Xunit;

namespace Tests
{
    public class BabySitterFeeCalculatorTests
    {
        BabySitterFeeCalculator babySitterFeeCalculator = new BabySitterFeeCalculator();

        [Theory]
        [InlineData("1:00AM", "10:00PM", "2:00AM", 16)]
        [InlineData("2:00AM", "1:00AM", "3:00AM", 16)]
        [InlineData("1:00AM", "1:00AM", "3:00AM", 32)]
        [InlineData("1:00AM", "2:00AM", "3:00AM", 32)]
        [InlineData("1:00AM", "3:00AM", "3:00AM", 32)]
        [InlineData("12:00AM", "1:00AM", "3:00AM", 48)]
        public void WhenCalculateRateGivenStartTimeAndEndTimeAfterMidNight(string value1, string value2, string value3, int value4)
        {
            var result = babySitterFeeCalculator.CalculateRate(value1, value2, value3);
            result.ShouldBe(value4);
        }
        [Theory]
        [InlineData("5:00PM", "3:00PM", "11:00PM", 48)]
        [InlineData("5:00PM", "5:00PM", "11:00PM", 48)]
        [InlineData("5:00PM", "3:00PM", "1:00AM", 72)]
        [InlineData("5:00PM", "5:00PM", "1:00AM", 72)]
        [InlineData("12:00AM", "10:00PM", "3:00AM", 48)]
        public void WhenCalculateRateGivenABedTimeSameOrBeforeStartTime(string value1, string value2, string value3, int value4)
        {
            var result = babySitterFeeCalculator.CalculateRate(value1, value2, value3);
            result.ShouldBe(value4);
        }
        [Theory]
        [InlineData("5:00PM", "11:00PM", "11:00PM", 72)]
        [InlineData("5:00PM", "1:00AM", "11:00PM", 72)]
        [InlineData("5:00PM", "1:00AM", "1:00AM", 100)]
        [InlineData("5:00PM", "1:00AM", "12:00AM", 84)]
        public void WhenCalculateRateGivenABedTimeSameOrAfterEndTime(string value1, string value2, string value3, int value4)
        {
            var result = babySitterFeeCalculator.CalculateRate(value1, value2, value3);
            result.ShouldBe(value4);
        }
        [Theory]
        [InlineData("10:00PM", "1:00AM", "2:00AM", 56)]
        [InlineData("5:00PM", "2:00AM", "1:00AM", 100)]
        public void WhenCalculateRateGivenStartTImeBeforeMidBedAndEndAfterMid(string value1, string value2, string value3, int value4)
        {
            var result = babySitterFeeCalculator.CalculateRate(value1, value2, value3);
            result.ShouldBe(value4);
        }
        [Theory]
        [InlineData("5:00PM", "6:00PM", "11:00PM", 52)]
        [InlineData("5:00PM", "10:00PM", "12:00AM", 76)]
        [InlineData("5:00PM", "5:00PM", "12:00AM", 56)]
        public void WhenCalculateRateGivenStartTimeBedTimeAndEndTimeBeforeOrAtMid(string value1, string value2, string value3, int value4)
        {
            var result = babySitterFeeCalculator.CalculateRate(value1, value2, value3);
            result.ShouldBe(value4);
        }
        [Theory]
        [InlineData("5:00PM", "6:00PM", "1:00AM", 76)]
        [InlineData("10:00PM", "12:00AM", "1:00AM", 40)]
        [InlineData("10:00PM", "12:00AM", "12:00AM", 24)]
        [InlineData("5:00PM", "7:00PM", "1:00AM", 80)]
        public void WhenCalculateRateGivenStartTimeAndBedTimeBeforeOrMidAndEndTimeAtOrAfterMid(string value1, string value2, string value3, int value4)
        {
            var result = babySitterFeeCalculator.CalculateRate(value1, value2, value3);
            result.ShouldBe(value4);
        }
        [Theory]
        [InlineData("1:30AM", "10:00PM", "2:00AM")]
        [InlineData("1:00AM", "10:00PM", "2:30AM")]
        [InlineData("1:00AM", "10:30PM", "2:00AM")]
        public void WhenCalculateRateGivenPartialHourWillReturn0(string value1, string value2, string value3)
        {
            var result = babySitterFeeCalculator.CalculateRate(value1, value2, value3);
            result.ShouldBe(0);
        }
    }
}
