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
        public ActionResult Create(PhotoCreate photoModel, TagCreate tagModel, PhotoEdit photoId, TagEdit tagId, PhotoTagCreate photoTagModel)
        {
            var photoService = CreatePhotoService();
            var tagService = CreateTagService();
            var photoTagService = CreatePhotoTagService();

            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];
                    if (file.ContentLength > 0)
                    {

                        var fileName = Path.GetFileName(file.FileName);
                        var nameOnly = Path.GetFileName(file.FileName);
                        photoModel.Image = Path.Combine(Server.MapPath("~/UploadedPhotos/"), fileName.TrimStart('/'));
                        file.SaveAs(photoModel.Image);
                        photoModel.Image = nameOnly;
                        photoService.CreatePhoto(photoModel);
                        tagService.CreateTag(tagModel);
                        int newPhotoId = photoService.GetPhotoId(photoModel.Image);
                        int newTagId = tagService.GetTagId(tagModel.TagName);
                        
                        //photoTagModel.PhotoId = photoId.PhotoId;//i need this to be the photo id of photomodel from photodetails?
                        //photoTagModel.TagId = tagId.TagId;
                        photoTagService.CreatePhotoTagBackground(newPhotoId, newTagId);
                    }
                    return RedirectToAction("Index");
                }
            }

            return View(photoModel);
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
                PhotoDate = detail.PhotoDate,
                Image = detail.Image
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PhotoEdit model, PhotoCreate photoModel)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreatePhotoService();
            int newPhotoId = service.GetPhotoId(photoModel.Image);
            model.PhotoId = newPhotoId;

            if (model.PhotoId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }


            service.UpdatePhoto(model);
            TempData["SaveResult"] = "Photo has been updated";
            return RedirectToAction("Index");

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Photo could not be updated");
                return View();
            }
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
            var photoService = new PhotoService(adminId);
            return photoService;
        }
        
        private TagService CreateTagService()
        {
            var adminId = Guid.Parse(User.Identity.GetUserId());
            var tagService = new TagService(adminId);
            return tagService;
        }

        private PhotoTagService CreatePhotoTagService()
        {
            var adminId = Guid.Parse(User.Identity.GetUserId());
            var photoTagService = new PhotoTagService(adminId);
            return photoTagService;
        }
    }
}