using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiteMVC.Repositories;

namespace SiteMVC.Controllers {
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
        public ActionResult Create(int id) {
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
        public ActionResult Edit(int id, Couleur couleur) {
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
            return View();
        }

        // POST: Couleurs/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection) {
            try {
                _repository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }
    }
}
