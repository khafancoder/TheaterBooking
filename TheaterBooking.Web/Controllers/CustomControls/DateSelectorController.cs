using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheaterBooking.Web.Utils;

namespace CannonDell.Web.Controllers
{
    public class DateSelectorController : Controller
    {

        public ActionResult Convert(string paramsJSON)
        {
            dynamic parameters = DynamicJSON.Parse(paramsJSON);

            int y = System.Convert.ToInt32(parameters.Year);
            int m = System.Convert.ToInt32(parameters.Month);
            int d = System.Convert.ToInt32(parameters.Day);
            string cultureName = parameters.Culture;

            AdaptedCultureInfo culture = new AdaptedCultureInfo(cultureName);
            System.Globalization.Calendar calendar = culture.Calendar;

            DateTime result = calendar.ToDateTime(y, m, d, 0, 0, 0, 0);

            return Content(result.ToShortDateString());
        }

        public ActionResult GetDaysInMonth(int year, int month, string culture)
        {
            AdaptedCultureInfo currentCulture = new AdaptedCultureInfo(culture);
            System.Globalization.Calendar calendar = currentCulture.Calendar;
            int result = calendar.GetDaysInMonth(year, month);

            return Content(result.ToString());
        }
    }


}
