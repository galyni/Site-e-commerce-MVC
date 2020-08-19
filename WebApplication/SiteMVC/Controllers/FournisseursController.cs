using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiteMVC.Repositories;

namespace SiteMVC.Controllers {
    public class FournisseursController : Controller {
        IRepository<Fournisseur> _repository;
        public FournisseursController(IRepository<Fournisseur> repository) {
            _repository = repository;
        }
        // GET: Fournisseurs
        public ActionResult Index() {
            var liste = _repository.GetList();
            return View(liste);
        }

        // GET: Fournisseurs/Create
        public ActionResult Create() {
            return View();
        }

        // POST: Fournisseurs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Fournisseur fournisseur) {
            try {
                _repository.Create(fournisseur);
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }

        // GET: Fournisseurs/Edit/5
        public ActionResult Edit(int id) {
            Fournisseur Fournisseur = _repository.GetById(id);
            return View(Fournisseur);
        }

        // POST: Fournisseurs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Fournisseur fournisseur) {
            try {
                _repository.Update(fournisseur);
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }

        // GET: Fournisseurs/Delete/5
        public ActionResult Delete(int id) {
            var item = _repository.GetById(id);
            return View(item);
        }

        // POST: Fournisseurs/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Fournisseur fournisseur) {
            try {
                _repository.Delete(fournisseur);
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }
    }
}
