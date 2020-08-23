using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SiteMVC.Models.ViewModels;
using SiteMVC.Repositories;

namespace SiteMVC.Controllers {
    [Authorize(Roles ="Administrator")]
    public class PhotosController : Controller {
        IRepository<Photo> _photoRepository;

        public PhotosController(IRepository<Photo> photoRepository) {
            _photoRepository = photoRepository;
        }

        // GET: Tirelires
        public IActionResult Index() {
            var liste = _photoRepository.GetList();
            return View(liste);
        }


        //public ActionResult Details(int id) {
        //    return View(_repository.GetById(id));
        //}


        public ActionResult Create() {
            ViewBag.IdProduit = new Repository<Produit>().GetList().Select(
                p => new SelectListItem { Value = p.Id.ToString(), Text = p.Nom });
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PhotoViewModel model) {
            // TODO : modifier toutes les methodes de tous les controllers sur ce shéma ?
            if (ModelState.IsValid && model.PhotoFile != null && model.PhotoFile.Length > 0) {
                byte[] image;
                using (var memoryStream = new MemoryStream()) {
                    model.PhotoFile.CopyTo(memoryStream);
                    image = memoryStream.ToArray();
                }
                Photo photo = new Photo() { IdProduit = model.IdProduit, Image = image, Nom=model.Nom };
                _photoRepository.Create(photo);
                return RedirectToAction(nameof(Index));
            }

            return View(model);

        }

        [HttpGet]
        public ActionResult Edit(int id) {
            ViewBag.IdProduit = new Repository<Produit>().GetList().Select(
                p => new SelectListItem { Value = p.Id.ToString(), Text = p.Nom });
            return View(_photoRepository.GetById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Photo photo) {
            try {
                _photoRepository.Update(photo);
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View(photo);
            }
        }

        public ActionResult Delete(int id) {
            return View(_photoRepository.GetById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Photo photo) {
            try {
                _photoRepository.Delete(photo);
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }
        [AllowAnonymous]
        public ActionResult GetImage(int id) {
            Photo photo = _photoRepository.GetById(id);
            //string imageDataUrl = "";
            //string imageBase64 = Convert.ToBase64String(photo.Image);
            //imageDataUrl = string.Format("data:image/jpg;base64,{0}", imageBase64);
            //var arr = Convert.FromBase64String(imageBase64);
            return File(photo.Image, "image/jpg", String.Concat(photo.Nom + ".jpg"));
        }
    }
}