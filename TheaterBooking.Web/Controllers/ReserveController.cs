using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheaterBooking.Entities;
using TheaterBooking.Infrastructure;
using System.Web.Security;

namespace TheaterBooking.Web.Controllers
{
    public class ReserveController : Controller
    {
        private readonly IRepository<Show> showRepository;
        private readonly IRepository<ShowTime> showTimeRepository;
        private readonly IRepository<Salon> salonRepository;
        private readonly IRepository<Reservation> reservationRepository;

        public ReserveController(IRepository<Show> showRepos, IRepository<Salon> salonRepos, IRepository<ShowTime> showTimeRepos, IRepository<Reservation> reservationRepos)
        {
            this.showRepository = showRepos;
            this.salonRepository = salonRepos;
            this.showTimeRepository = showTimeRepos;
            this.reservationRepository = reservationRepos;
        }


        [HttpGet]
        public ActionResult Reserve(int Id, bool? archive)
        {
            var show = showRepository.Get(Id);
            if (show != null)
            {
                var showTimes = archive.GetValueOrDefault() ? show.ShowTimes : show.ShowTimes.Where(o => o.ShowDateTime >= DateTime.Now);
                showTimes = showTimes.OrderBy(o => o.ShowDateTime);

                IList<SelectListItem> showTimesSelectList = new List<SelectListItem>();
                foreach (var item in showTimes)
                {
                    showTimesSelectList.Add(new SelectListItem()
                    {
                        Text = string.Format("{0} / ساعت {1:hh:mm} - {2}", item.ShowDateTime.ToFullPersian(), item.ShowDateTime, item.Salon.Title),
                        Value = string.Format("{{\"ShowTimeId\":\"{0}\", \"SalonId\":\"{1}\"}}", item.ShowTimeId, item.Salon.SalonId)
                    });
                }

                ViewBag.ShowTimesSelectList = showTimesSelectList;
                ViewBag.Show = show;
                ViewBag.ArchiveMode = archive.GetValueOrDefault();

                return View();

            }
            else
                return HttpNotFound();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Reserve(int ShowTimeId, string ReserveInfo, int SeatRow, int SeatCol)
        {
            var showTime = showTimeRepository.Get(ShowTimeId);
            if (showTime != null)
            {
                if (reservationRepository.Count(o =>
                    o.ShowTime == showTime &&
                    o.SeatRow == SeatRow &&
                    o.SeatColumn == SeatCol) < 1)
                {
                    Reservation r = new Reservation();
                    r.ShowTime = showTime;
                    r.ReserveInfo = ReserveInfo;
                    r.SeatRow = SeatRow;
                    r.SeatColumn = SeatCol;
                    r.ReserveDateTime = DateTime.UtcNow;
                    r.ReservedByUserId = (Guid)Membership.GetUser().ProviderUserKey;
                    reservationRepository.Save(r);

                    return Json("success");
                }
                else
                {
                    return Json("error: can't reserve");
                }
            }
            else
                return HttpNotFound();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Unreserve(int ReservationId)
        {
            var reservation = reservationRepository.Get(ReservationId);

            if (reservation != null)
            {
                if ((reservation.ReservedByUserId == (Guid)Membership.GetUser().ProviderUserKey)
                    ||
                    (Roles.IsUserInRole(User.Identity.Name, "admin")))
                {
                    reservationRepository.Delete(ReservationId);
                    return Json("success");
                }
                else
                    return Json("failed");
            }
            else
                return Json("failed");
        }


        #region Ajax Calls

        [HttpGet]
        public JsonResult ReservedSeats(int ShowTimeId)
        {
            var showTime = showTimeRepository.Get(ShowTimeId);
            if (showTime != null)
            {
                var reservations = reservationRepository.Find(o => o.ShowTime == showTime);
                IList<ViewModels.ReservedSeatViewModel> result = new List<ViewModels.ReservedSeatViewModel>();

                foreach (var item in reservations)
                {
                    result.Add(new ViewModels.ReservedSeatViewModel()
                    {
                        ReservationId = item.ReservationId,
                        Row = item.SeatRow,
                        Column = item.SeatColumn,
                        ReservationInfo = item.ReserveInfo,
                        Username = User.Identity.IsAuthenticated ? Membership.GetUser(item.ReservedByUserId).UserName : "null"
                    });
                }

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
                return null;
        }

        [HttpGet]
        [Authorize]
        public JsonResult Get(int Id)
        {
            var reservation = reservationRepository.Get(Id);

            if (reservation != null)
            {
                return Json(new ViewModels.ReservedSeatViewModel()
                    {
                        ReservationId = reservation.ReservationId,
                        ReservationInfo = reservation.ReserveInfo,
                        Column = reservation.SeatColumn,
                        Row = reservation.SeatRow,
                        Username = Membership.GetUser(reservation.ReservedByUserId).UserName

                    }, JsonRequestBehavior.AllowGet);
            }
            else
                return null;
        }

        #endregion

    }
}
