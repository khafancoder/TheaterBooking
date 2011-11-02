using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheaterBooking.Entities;
using TheaterBooking.Infrastructure;

namespace TheaterBooking.Web.Controllers
{
    public class ShowController : Controller
    {
        private readonly IRepository<Show> showRepository;

        public ShowController(IRepository<Show> showRepos)
        {
            this.showRepository = showRepos;
        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Manage()
        {
            var shows = showRepository.List().OrderByDescending(o => o.CreationDate);

            return View(shows);
        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [Authorize(Roles = "admin")]
        public ActionResult Create(Show model)
        {
            if (ModelState.IsValid)
            {
                Show newShow = new Show();
                newShow.IsActive = true;
                newShow.Title = model.Title;
                newShow.ImageUrl = model.ImageUrl;
                newShow.Description = model.Description;
                newShow.CreationDate = DateTime.UtcNow;

                this.showRepository.Save(newShow);

                return RedirectToAction("Manage");
            }
            else
            {
                ModelState.AddModelError("", "ابتدا مقادیر خواسته شده را وارد نمایید");
                return View();
            }
        }



        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int Id)
        {
            var show = showRepository.Get(Id);
            if (show != null)
            {
                return View(show);
            }
            else
                return HttpNotFound();

        }

        [HttpPost]
        [ValidateInput(false)]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(Show model)
        {
            var show = showRepository.Get(model.ShowId);
            if (show != null)
            {
                if (ModelState.IsValid)
                {
                    show.Title = model.Title;
                    show.ImageUrl = model.ImageUrl;
                    show.Description = model.Description;
                    showRepository.Save(show);

                    return RedirectToAction("Manage");
                }
                else
                {
                    ModelState.AddModelError("", "ابتدا مقادیر خواسته شده را وارد نمایید");
                    return View();
                }
            }
            else
                return HttpNotFound();

        }



        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int Id)
        {
            var show = showRepository.Get(Id);
            if (show != null)
            {
                showRepository.Delete(Id);
                return RedirectToAction("Manage");
            }
            else
                return HttpNotFound();

        }

    }
}
