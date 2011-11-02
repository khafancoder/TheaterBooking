using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TheaterBooking.Entities;

namespace TheaterBooking.Web.ViewModels
{
    public class SingleShowTime
    {

        public int SalonId { get; set; }
        public int ShowId { get; set; }

        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
    }

    public class RepeatedShowTime
    {
        public int SalonId { get; set; }
        public int ShowId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime Time { get; set; }
    }

}
