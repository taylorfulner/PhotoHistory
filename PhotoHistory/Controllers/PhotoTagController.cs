using Microsoft.AspNet.Identity;
using PhotoHistory.Models;
using PhotoHistory.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoHistory.Controllers
{
    [Authorize]
    public class PhotoTagController : Controller
    {
        // GET: PhotoTagJoin
        public ActionResult Index()
        {
            var service = CreatePhotoTagService();
            var model = service.GetPhotoTags();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PhotoTagCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreatePhotoTagService();

            if (service.CreatePhotoTag(model))
            {
                TempData["SaveResult"] = "Tag was added to photo.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Tag could not be added to photo.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var service = CreatePhotoTagService();
            var model = service.GetPhotoTagById(id);

            return View(model);
        }

        //public ActionResult Edit(int id)
        //{
        //    var service = CreatePhotoTagService();
        //    var detail = service.GetPhotoTagById(id);
        //    var model = new PhotoTagEdit
        //    {
        //        PhotoTagId = detail.PhotoTagId,
        //        PhotoId = detail.PhotoId,
        //        TagId = detail.TagId
        //    };

        //    return View(model);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, PhotoTagEdit model)
        //{
        //    if (!ModelState.IsValid) return View(model);

        //    if (model.PhotoTagId != id)
        //    {
        //        ModelState.AddModelError("", "Id Mismatch");
        //        return View(model);
        //    }

        //    var service = CreatePhotoTagService();

        //    if (service.UpdatePhotoTag(model))
        //    {
        //        TempData["SaveResult"] = "Your tag was updated to the correct photo";
        //        return RedirectToAction("Index");
        //    }

        //    ModelState.AddModelError("", "Your tag could not be updated to the correct photo");
        //    return View();
        //}

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreatePhotoTagService();
            var model = service.GetPhotoTagById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTagFromPhoto(int id)
        {
            var service = CreatePhotoTagService();

            service.DeletePhotoTag(id);

            TempData["SaveResult"] = "Your tag was deleted from the photo";

            return RedirectToAction("Index");
        }

        private PhotoTagService CreatePhotoTagService()
        {
            var adminId = Guid.Parse(User.Identity.GetUserId());
            var photoTagService = new PhotoTagService(adminId);
            return photoTagService;
        }
    }
}