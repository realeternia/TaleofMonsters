using System;

namespace NarlonLib.Core
{
    public class TimeTool
    {
        public static long GetNowMiliSecond()
        {
            return DateTime.Now.Ticks/10000;
        }

        public static DateTime UnixTimeToDateTime(int sec)
        {
            DateTime start = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return start.AddSeconds(sec);
        }

        public static int DateTimeToUnixTime(DateTime time)
        {
            DateTime start = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (int) (time - start).TotalSeconds;
        }
    }
}
