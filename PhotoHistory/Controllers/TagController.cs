using Microsoft.AspNet.Identity;
using PhotoHistory.Models;
using PhotoHistory.Services;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoHistory.Controllers
{
    [Authorize]
    public class TagController : Controller
    {
        public ActionResult Index()
        {
            var service = CreateTagService();
            var model = service.GetTags();
            ViewBag.TagNames = service.GetTagNames();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TagCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateTagService();

            if (service.CreateTag(model))
            {
                TempData["SaveResult"] = "Tag was successfully created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Tag could not be added.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var service = CreateTagService();
            var model = service.GetTagById(id);

            return View(model);
        }



        public ActionResult Edit(int id)
        {
            var service = CreateTagService();
            var detail = service.GetTagById(id);
            var model = new TagEdit
            {
                TagId = detail.TagId,
                TagName = detail.TagName,
                TagType = detail.TagType
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TagEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.TagId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateTagService();

            if (service.UpdateTag(model))
            {
                TempData["SaveResult"] = "Your tag was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your tag could not be updated.");
            return View();
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreateTagService();
            var model = service.GetTagById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTag(int id)
        {
            var service = CreateTagService();

            service.DeleteTag(id);

            TempData["SaveResult"] = "Your tag was deleted.";

            return RedirectToAction("Index");
        }

        private TagService CreateTagService()
        {
            var adminId = Guid.Parse(User.Identity.GetUserId());
            var service = new TagService(adminId);
            return service;
        }
    }
}