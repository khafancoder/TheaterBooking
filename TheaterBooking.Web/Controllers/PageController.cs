using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TheaterBooking.Infrastructure;
using TheaterBooking.Entities;


namespace TheaterBooking.Web.Controllers
{
    public class PageController : Controller
    {
        private readonly IRepository<DynamicPage> _dynamicPageRepository;
        public PageController(IRepository<DynamicPage> dynamicPageRepos)
        {
            _dynamicPageRepository = dynamicPageRepos;
        }


        private bool NameDoesExists(string pageName)
        {
            return _dynamicPageRepository.Count(o => o.Name == pageName.ToLower()) > 0;
        }
        private DynamicPage GetByName(string name)
        {
            DynamicPage item = _dynamicPageRepository.FindOne(o => o.Name == name.ToLower());
            return item;
        }



        [HttpGet]
        public ActionResult Show(string id)
        {
            var item = GetByName(id);

            if (item != null)
                return View(item);
            else
                return HttpNotFound();
        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Manage()
        {
            var pages = _dynamicPageRepository.List();
            return View(pages);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateInput(false)]
        public ActionResult Create(DynamicPage item)
        {
            if (ModelState.IsValid)
            {
                item.Name = item.Name.Replace(" ", "_");

                if (!NameDoesExists(item.Name))
                {
                    DynamicPage newPage = new DynamicPage();
                    newPage.Name = item.Name.ToLower();
                    newPage.LastUpdate = newPage.CreationDate = DateTime.UtcNow;
                    newPage.Title = item.Title;
                    newPage.Summary = item.Summary;
                    newPage.Text = item.Text;
                    newPage.Tags = item.Tags;

                    _dynamicPageRepository.Save(newPage);

                    return RedirectToAction("Manage");
                }
                else
                {
                    ModelState.AddModelError("", "صفحه دیگری با نام انتخاب شده وجود دارد، از نام دیگری استفاده نمایید");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "ابتدا اطلاعات خواسته شده را وارد نمایید");
                return View();
            }


        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(string id)
        {
            var item = GetByName(id);

            if (item != null)
                return View(item);
            else
                return HttpNotFound();
        }


        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateInput(false)]
        public ActionResult Edit(string id, DynamicPage item)
        {
            var oldItem = GetByName(id);

            if (oldItem != null)
            {
                oldItem.Title = item.Title;
                oldItem.Summary = item.Summary;
                oldItem.Text = item.Text;
                oldItem.Tags = item.Tags;
                oldItem.LastUpdate = DateTime.UtcNow;

                _dynamicPageRepository.Save(oldItem);

                return RedirectToAction("Manage");
            }
            else
                return HttpNotFound();
        }


        [Authorize(Roles = "admin")]
        public ActionResult Delete(string id)
        {
            var item = GetByName(id);

            if (item != null)
            {
                _dynamicPageRepository.Delete(item.DynamicPageId);
                return RedirectToAction("Manage");
            }
            else
                return HttpNotFound();
        }



    }

}
