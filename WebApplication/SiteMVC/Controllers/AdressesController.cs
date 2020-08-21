using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiteMVC.Repositories;

namespace SiteMVC.Controllers {
    public class AdressesController : Controller {
        IRepository<Adresse> _repository;
        public AdressesController(IRepository<Adresse> repository) {
            _repository = repository;
        }
        // GET: Adresses
        public ActionResult Index() {
            var liste = _repository.GetList();
            return View(liste);
        }

        // GET: Adresses/Create
        public ActionResult Create() {
            return View();
        }

        // POST: Adresses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Adresse adresse) {
            try {
                _repository.Create(adresse);
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }

        // GET: Adresses/Edit/5
        public ActionResult Edit(int id) {
            Adresse adresse = _repository.GetById(id);
            return View(adresse);
        }

        // POST: Adresses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Adresse adresse) {
            try {
                _repository.Update(adresse);
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }

        // GET: Adresses/Delete/5
        public ActionResult Delete(int id) {
            var item = _repository.GetById(id);
            return View(item);
        }

        // POST: Adresses/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Adresse adresse) {
            try {
                // TODO : gestion exception mise à jour BD
                _repository.Delete(adresse);
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }
    }
}
