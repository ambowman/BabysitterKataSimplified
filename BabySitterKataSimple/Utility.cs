using System.Linq;

namespace BabySitterKataSimple
{
    public class Utility
    {
        public char[] MakeCharArray(string time)
        {
            char[] timeArray = new char[time.Length];
            return timeArray = time.ToCharArray();
        }
        public bool CheckAmOrPm(string time)
        {
            if (time.EndsWith("PM"))
            {
                return true;
            }
            return false;
        }
        public char[] RemoveColon(char[] timeArray)
        {
            int i = 0;
            for (; i < timeArray.Length; ++i)
            {
                if (timeArray[i] == ':')
                    timeArray[i] = '.';
            }
            return timeArray;
        }
        public char[] RemoveAmPm(char[] timeArray)
        {
            timeArray = timeArray.Where(val => val != 'A').ToArray();
            timeArray = timeArray.Where(val => val != 'P').ToArray();
            timeArray = timeArray.Where(val => val != 'M').ToArray();
            return timeArray;
        }
    }
}
