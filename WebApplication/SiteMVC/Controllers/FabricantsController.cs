using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiteMVC.Repositories;

namespace SiteMVC.Controllers {
    public class FabricantsController : Controller {
        IRepository<Fabricant> _repository;
        public FabricantsController(IRepository<Fabricant> repository) {
            _repository = repository;
        }
        // GET: Fabricants
        public ActionResult Index() {
            var liste = _repository.GetList();
            return View(liste);
        }

        // GET: Fabricants/Create
        public ActionResult Create() {
            return View();
        }

        // POST: Fabricants/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Fabricant fabricant) {
            try {
                _repository.Create(fabricant);
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }

        // GET: Fabricants/Edit/5
        public ActionResult Edit(int id) {
            Fabricant Fabricant = _repository.GetById(id);
            return View(Fabricant);
        }

        // POST: Fabricants/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Fabricant fabricant) {
            try {
                _repository.Update(fabricant);
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }

        // GET: Fabricants/Delete/5
        [HttpGet]
        public ActionResult Delete(int id) {
            var item = _repository.GetById(id);
            return View(item);
        }

        // POST: Fabricants/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Fabricant fabricant) {
            try {
                _repository.Delete(fabricant);
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }
    }
}
