using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiteMVC.Repositories;

namespace SiteMVC.Controllers {
    [Authorize(Roles ="Administrator")]
    public class CouleursController : Controller {
        IRepository<Couleur> _repository;
        public CouleursController(IRepository<Couleur> repository) {
            _repository = repository;
        }
        // GET: Couleurs
        public ActionResult Index() {
            var liste = _repository.GetList();
            return View(liste);
        }

        // GET: Couleurs/Create
        public ActionResult Create() {
            return View();
        }

        // POST: Couleurs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Couleur couleur) {
            try {
                _repository.Create(couleur);
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }

        // GET: Couleurs/Edit/5
        public ActionResult Edit(int id) {
            Couleur couleur = _repository.GetById(id);
            return View(couleur);
        }

        // POST: Couleurs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Couleur couleur) {
            try {
                _repository.Update(couleur);
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }

        // GET: Couleurs/Delete/5
        public ActionResult Delete(int id) {
            var item = _repository.GetById(id);
            return View(item);
        }

        // POST: Couleurs/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Couleur collection) {
            try {
                _repository.Delete(collection);
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }
    }
}
