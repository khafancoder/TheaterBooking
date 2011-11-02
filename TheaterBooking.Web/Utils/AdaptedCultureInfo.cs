using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheaterBooking.Web.Utils
{
    public class AdaptedCultureInfo : System.Globalization.CultureInfo
    {
        public AdaptedCultureInfo(string name)
            : base(name)
        {

            if (name.ToLower() == "fa-ir")
            {
                System.Globalization.DateTimeFormatInfo info = this.DateTimeFormat;
                info.AbbreviatedDayNames = new string[] { "ی", "د", "س", "چ", "پ", "ج", "ش" };
                info.DayNames = new string[] { "یکشنبه", "دوشنبه", "ﺳﻪشنبه", "چهارشنبه", "پنجشنبه", "جمعه", "شنبه" };
                info.AbbreviatedMonthNames = new string[] { "فروردین", "ارديبهشت", "خرداد", "تير", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند", "" };
                info.MonthNames = new string[] { "فروردین", "ارديبهشت", "خرداد", "تير", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند", "" };
                info.AMDesignator = "ق.ظ";
                info.PMDesignator = "ب.ظ";
                info.ShortDatePattern = "yyyy/MM/dd";
                info.FirstDayOfWeek = DayOfWeek.Saturday;

                System.Globalization.PersianCalendar cal = new System.Globalization.PersianCalendar();

                typeof(System.Globalization.DateTimeFormatInfo).GetField("calendar", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(info, cal);
                //object obj = typeof(System.Globalization.DateTimeFormatInfo).GetField("m_cultureTableRecord", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(info);
                //obj.GetType().GetMethod("UseCurrentCalendar", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(obj, new object[] { cal.GetType().GetProperty("ID", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(cal, null) });
                typeof(System.Globalization.CultureInfo).GetField("calendar", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(this, cal);
                typeof(System.Globalization.CultureInfo).GetField("calendar", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(this, cal);
            }
        }
    }
}
