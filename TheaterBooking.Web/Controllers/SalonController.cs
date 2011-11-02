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
    public class SalonController : Controller
    {
        private readonly IRepository<Salon> salonRepository;

        public SalonController(IRepository<Salon> salonRepos)
        {
            this.salonRepository = salonRepos;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Manage()
        {
            var salons = salonRepository.List();
            return View(salons);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Create(Salon model)
        {
            //dynamic seatsLayout = Utils.DynamicJSON.Parse(seatsLayoutJSON);

            var newSalon = new Salon();
            newSalon.Title = model.Title;
            newSalon.Address = model.Address;
            newSalon.Phone = model.Phone;

            salonRepository.Save(model);

            return RedirectToAction("Manage");
        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int Id)
        {
            var salon = salonRepository.Get(Id);
            if (salon != null)
            {
                salonRepository.Delete(Id);
            }
            else
                return HttpNotFound();

            return RedirectToAction("Manage");
        }


        [HttpGet]
        public JsonResult GetSeatLayout(int Id)
        {
            var salon = salonRepository.Get(Id);
            if (salon != null)
            {
                var rows = salon.SeatRows.OrderBy(o => o.RowIndex).ToList();
                int[] result = new int[rows.Count];
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = rows[i].SeatsCount;
                }

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
                return null;
        }
    }
}
