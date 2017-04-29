using System;

namespace BabySitterKataSimple
{
    public class TimeConverter
    {
        private Utility _utility;
        public TimeConverter()
        {
            _utility = new Utility();
        }
        public double ConvertTimeArrayToDouble(char[] timeArray)
        {
            double timeDouble = 0.0;
            string time = new string(timeArray);
            timeDouble = Double.Parse(time);
            return timeDouble;
        }
        public double MakeMilitaryTime(double doubleTimeDec, bool isPm)
        {
            if (!isPm && doubleTimeDec >= 12.0)
                return doubleTimeDec + 12.0;
            if (isPm && doubleTimeDec < 12.0)
                return doubleTimeDec + 12.0;
            return doubleTimeDec;
        }
        public double ConvertTimeString(string time)
        {
            double convertedDoubleTime = 0.0;
            time = time.ToUpper();
            bool isPm = _utility.CheckAmOrPm(time);
            char[] timeArray = _utility.MakeCharArray(time);
            timeArray = _utility.RemoveAmPm(timeArray);
            timeArray = _utility.RemoveColon(timeArray);
            convertedDoubleTime = ConvertTimeArrayToDouble(timeArray);
            convertedDoubleTime = MakeMilitaryTime(convertedDoubleTime, isPm);
            return convertedDoubleTime;
        }
        public double ConvertRawTimeDoubleToFractionalHours(double doubleTime)
        {
            double floorTime = 0.0;
            double fracTime = 0.0;
            floorTime = Math.Floor(doubleTime);
            fracTime = doubleTime - floorTime;
            doubleTime = Math.Round(((fracTime / .60) + floorTime), 2);
            return doubleTime;
        }
    }
}
