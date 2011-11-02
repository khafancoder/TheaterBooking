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
    public class ReservedSeatViewModel
    {
        public int ReservationId { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public string Username { get; set; }
        public string ReservationInfo { get; set; }
    }

}
