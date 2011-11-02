using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheaterBooking.Entities;
using TheaterBooking.Infrastructure;
using TheaterBooking.Web.ViewModels;

namespace TheaterBooking.Web.Controllers
{
    public class ShowTimeController : Controller
    {
        private readonly IRepository<Show> showRepository;
        private readonly IRepository<ShowTime> showTimeRepository;
        private readonly IRepository<Salon> salonRepository;

        public ShowTimeController(IRepository<Show> showRepos, IRepository<ShowTime> showTimeRepos, IRepository<Salon> salonRepos)
        {
            this.showRepository = showRepos;
            this.showTimeRepository = showTimeRepos;
            this.salonRepository = salonRepos;
        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult List(int Id)
        {
            var show = showRepository.Get(Id);
            if (show != null)
            {
                var showtimes = showTimeRepository.Find(o => o.Show == show).OrderByDescending(o => o.ShowTimeId);

                ViewBag.Show = show;
                return View(showtimes);
            }
            else
                return HttpNotFound();
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Create(int Id)
        {
            var show = showRepository.Get(Id);
            var salons = salonRepository.List();

            if (show != null)
            {
                ViewBag.Show = show;
                ViewBag.Salons = salons;

                return View();
            }
            else
                return HttpNotFound();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult CreateSingle(SingleShowTime model)
        {
            DateTime showDate = model.Date;

            ShowTime newShowTime = new ShowTime();
            newShowTime.Show = showRepository.Get(model.ShowId);
            newShowTime.Salon = salonRepository.Get(model.SalonId);
            newShowTime.ShowDateTime = new DateTime(showDate.Year, showDate.Month, showDate.Day, model.Time.Hour, model.Time.Minute, model.Time.Second);
            newShowTime.ShowDateSh = showDate.ToPersian();
            this.showTimeRepository.Save(newShowTime);

            return RedirectToAction("List", new { Id = model.ShowId });
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult CreateRepeated(RepeatedShowTime model)
        {
            DateTime endDate = model.EndDate;
            DateTime startDate = model.StartDate;
            Show show = showRepository.Get(model.ShowId);
            Salon salon = salonRepository.Get(model.SalonId);


            if (endDate > startDate)
            {
                DateTime showDate = startDate;
                while (showDate <= endDate)
                {
                    ShowTime newShowTime = new ShowTime();
                    newShowTime.Show = show;
                    newShowTime.Salon = salon;
                    newShowTime.ShowDateTime = new DateTime(showDate.Year, showDate.Month, showDate.Day, model.Time.Hour, model.Time.Minute, model.Time.Second);
                    newShowTime.ShowDateSh = showDate.ToPersian();
                    this.showTimeRepository.Save(newShowTime);

                    showDate = showDate.AddDays(1);
                }

                return RedirectToAction("List", new { Id = model.ShowId });
            }
            else
                throw new Exception("Invalid Date Range");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int Id)
        {
            var showTime = showTimeRepository.Get(Id);
            if (showTime != null)
            {
                showTimeRepository.Delete(Id);
                return RedirectToAction("List", "ShowTime", new { Id = showTime.Show.ShowId });
            }
            else
                return HttpNotFound();
        }
    }
}
