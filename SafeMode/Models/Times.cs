using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SafeMode.Models
{
    public class Times
    {
        

        public static DateTime getQatarTime()
        {
            //public DateTime QatarTime;
            string tz = "Arabic Standard Time";
            TimeZoneInfo myZone = TimeZoneInfo.FindSystemTimeZoneById(tz);
            return  TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, myZone);
           
        }
    }
}