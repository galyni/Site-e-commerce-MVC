using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiteMVC.Repositories;

namespace SiteMVC.Controllers {
    public class CommandesController : Controller {         // 2 controllers parce que 2 repo ?
        private IRepository<Commande> _depotCommades;
        private IRepository<DetailCommande> _depotDetail;
        public CommandesController(IRepository<Commande> depotCommandes, IRepository<DetailCommande> depotDetail) {
            _depotCommades = depotCommandes;
            _depotDetail = depotDetail;
        }
        // GET: CommandesController
        public ActionResult Index() {
            return View();
        }

        // GET: CommandesController/Details/5
        public ActionResult Details(int id) {
            return View();
        }

        // GET: CommandesController/Create
        public ActionResult Create() {
            return View();
        }

        // POST: CommandesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id) {
            try {
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }

        // GET: CommandesController/Edit/5
        public ActionResult Edit(int id) {
            return View();
        }

        // POST: CommandesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection) {
            try {
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }

        // GET: CommandesController/Delete/5
        public ActionResult Delete(int id) {
            return View();
        }

        // POST: CommandesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection) {
            try {
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }
    }
}
