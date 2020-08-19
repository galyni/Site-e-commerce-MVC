using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiteMVC.Repositories;

namespace SiteMVC.Controllers {
    public class CategoriesController : Controller {
        IRepository<Categorie> _repository;
        public CategoriesController(IRepository<Categorie> repository) {
            _repository = repository;
        }
        // GET: Categories
        public ActionResult Index() {
            var liste = _repository.GetList();
            return View(liste);
        }

        // GET: Categories/Create
        public ActionResult Create() {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Categorie categorie) {
            try {
                _repository.Create(categorie);
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int id) {
            Categorie categorie = _repository.GetById(id);
            return View(categorie);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categorie categorie) {
            try {
                _repository.Update(categorie);
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }

        // GET: Categories/Delete/5
        [HttpGet]
        public ActionResult Delete(int id) {
            var item = _repository.GetById(id);
            return View(item);
        }

        // POST: Categories/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Categorie categorie) {
            try {
                _repository.Delete(categorie);
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }
    }
}
