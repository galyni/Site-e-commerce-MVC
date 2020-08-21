using Castle.Core.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SiteMVC.Repositories;
using System.Collections.Generic;

namespace SiteMVC.Controllers {
    public class PanierController : Controller {
        IRepository<Produit> _repository;           //Possible de passer le modele à AddToCart

        public PanierController(IRepository<Produit> produitRepository) {
            _repository = produitRepository;
        }

        // TODO : vue partielle ou modale pour choix quantite ?
        //[HttpGet]
        //public ActionResult AddToCart(int id) {        // ajouter la quantite a Details, voire creer une vue intermediaire
        //    
        //    return RedirectToAction("Index", "Tirelires");      // Retour à la page gallerie
        //}

        [HttpPost]
        public ActionResult AddToCart(int id, int quantite) {
            string currentCartSerialized = HttpContext.Session.GetString("Cart");
            List<KeyValuePair<int, int>> currentCart;
            if (currentCartSerialized.IsNullOrEmpty()) {
                currentCart = new List<KeyValuePair<int, int>>();
                currentCart.Add(new KeyValuePair<int, int>(id, quantite));
            }
            else {
                currentCart= JsonConvert.DeserializeObject<List<KeyValuePair<int, int>>>(currentCartSerialized);
                // TODO : cas où l'objet est déjà commandé : ajouter la quantité
                currentCart.Add(new KeyValuePair<int, int>(id, quantite));
            }
            currentCartSerialized = JsonConvert.SerializeObject(currentCart);
            HttpContext.Session.SetString("Cart", currentCartSerialized);
            return RedirectToAction("Index", "Tirelires");
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
