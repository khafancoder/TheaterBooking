using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Linq.Expressions;
using System.Globalization;

namespace TheaterBooking.Web
{
    public static class DateHelper
    {
        private static CultureInfo persianCulture = new Utils.AdaptedCultureInfo("fa-IR");

        public static string ToPersian(this DateTime dt)
        {
            Calendar calendar = persianCulture.Calendar;

            return string.Format("{0}/{1}/{2}",
                calendar.GetYear(dt),
                calendar.GetMonth(dt),
                calendar.GetDayOfMonth(dt));
        }

        public static string ToFullPersian(this DateTime dt)
        {
            Calendar calendar = persianCulture.Calendar;

            string result = "{0} {1} {2} {3}";

            result = string.Format(result,
                persianCulture.DateTimeFormat.DayNames[(int)calendar.GetDayOfWeek(dt)],
                calendar.GetDayOfMonth(dt),
                 persianCulture.DateTimeFormat.MonthNames[calendar.GetMonth(dt) - 1],
                calendar.GetYear(dt));

            return result;
        }
    }
}