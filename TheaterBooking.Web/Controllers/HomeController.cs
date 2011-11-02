using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheaterBooking.Infrastructure;
using TheaterBooking.Entities;

namespace TheaterBooking.Web.Controllers
{

    public class HomeController : Controller
    {
        private readonly IRepository<Show> showRepository;

        public HomeController(IRepository<Show> repository)
        {
            showRepository = repository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var activeShow = showRepository.Find(o => o.IsActive == true).OrderByDescending(o => o.CreationDate).FirstOrDefault();
            ViewBag.ActiveShow = activeShow;

            return View();
        }

        [HttpGet]
        public ActionResult UnderConstruction()
        {
            return View();
        }
    }
}
