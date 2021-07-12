using Microsoft.AspNet.Identity;
using PhotoHistory.Models;
using PhotoHistory.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoHistory.Controllers
{
    [Authorize]
    public class PhotoController : Controller
    {
        public ActionResult Index()
        {
            var service = CreatePhotoService();
            var model = service.GetPhotos();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PhotoCreate model)
        {
            var service = CreatePhotoService();

            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];
                    if (file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var nameOnly = Path.GetFileName(file.FileName);
                        model.Image = Path.Combine(Server.MapPath("~/UploadedPhotos"), fileName.TrimStart('/'));
                        file.SaveAs(model.Image);
                        model.Image = nameOnly;
                        service.CreatePhoto(model);
                    }
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var service = CreatePhotoService();
            var model = service.GetPhotoById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreatePhotoService();
            var detail = service.GetPhotoById(id);
            var model = new PhotoEdit
            {
                PhotoId = detail.PhotoId,
                PhotoName = detail.PhotoName,
                PhotoDesc = detail.PhotoDesc,
                PhotoDate = detail.PhotoDate
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PhotoEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.PhotoId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreatePhotoService();

            if (service.UpdatePhoto(model))
            {
                TempData["SaveResult"] = "Photo has been updated";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Photo could not be updated");
            return View();
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreatePhotoService();
            var model = service.GetPhotoById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePhoto(int id)
        {
            var service = CreatePhotoService();

            service.DeletePhoto(id);

            TempData["SaveResult"] = "Your photo was deleted";

            return RedirectToAction("Index");
        }

        private PhotoService CreatePhotoService()
        {
            var adminId = Guid.Parse(User.Identity.GetUserId());
            var service = new PhotoService(adminId);
            return service;
        }
    }
}