using Microsoft.AspNetCore.Mvc;
using SiteMVC.Repositories;

namespace SiteMVC.Controllers {
    public class PanierController : Controller {
        IRepository<Produit> _repository;           //Possible de passer le modele à AddToCart

        public PanierController(IRepository<Produit> produitRepository) {
            _repository = produitRepository;
        }
        public ActionResult AddToCart(int id, int quantite) {        // ajouter la quantite a Details, voire creer une vue intermediaire
            var produit = _repository.GetById(id);
            // TODO ajouter la logique pour ajout a la session 
            return RedirectToAction("Index", "Tirelires");      // Retour à la page gallerie
        }
        // GET: PanierController
        public ActionResult Panier() {
            return View();
        }

        // GET: PanierController/Details/5
        public ActionResult Details(int id) {
            return View();
        }

        // GET: PanierController/Create
        //public ActionResult Create() {
        //    return View();
        //}

        //// POST: PanierController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection) {
        //    try {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch {
        //        return View();
        //    }
        //}

        // GET: PanierController/Edit/5
        public ActionResult Edit(int id) {
            return View();
        }

        // POST: PanierController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection) {
        //    try {
        //        //return RedirectToAction(nameof(Index));
        //    }
        //    catch {
        //        return View();
        //    }
        //}

        //// GET: PanierController/Delete/5
        //public ActionResult Delete(int id) {
        //    return View();
        //}

        //// POST: PanierController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection) {
        //    try {
        //        //return RedirectToAction(nameof(Index));
        //    }
        //    catch {
        //        return View();
        //    }
        //}
    }
}
