using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Mvc.Ajax;
using TheaterBooking.Web.Utils;

namespace TheaterBooking.Web
{
    public static class CustomControlsHelper
    {
        public static HtmlString DateSelector(this HtmlHelper helper, string cultureCode, string fieldName, int pastOffset, int futureOffset)
        {
            const string VIEW_FILE_PATH = "_CC_DateSelector";


            Utils.AdaptedCultureInfo currentCulture = new AdaptedCultureInfo(cultureCode);
            System.Globalization.Calendar calendar = currentCulture.Calendar;

            int endYear = calendar.GetYear(DateTime.UtcNow) + futureOffset;
            int startYear = calendar.GetYear(DateTime.UtcNow) - pastOffset;
            string[] monthNames = currentCulture.DateTimeFormat.MonthNames;


            var url = new UrlHelper(helper.ViewContext.RequestContext);

            return helper.Partial(VIEW_FILE_PATH,
                new
                {
                    Name = fieldName,
                    StartYear = startYear,
                    EndYear = endYear,
                    Months = monthNames,
                    CurrentCulture = cultureCode,
                    ConvertorURL = url.Action("Convert", "DateSelector"),
                    DaysInMonthURL = url.Action("GetDaysInMonth", "DateSelector")
                });
        }

        public static HtmlString TimeSelector(this HtmlHelper helper, string fieldName)
        {
            const string VIEW_FILE_PATH = "_CC_TimeSelector";

            return helper.Partial(VIEW_FILE_PATH,
                new
                {
                    Name = fieldName
                });
        }

    }

}