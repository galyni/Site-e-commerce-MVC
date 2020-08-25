using Castle.Core.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SiteMVC.Models.ViewModels;
using SiteMVC.Repositories;
using System.Collections.Generic;

namespace SiteMVC.Controllers {
    public class PanierController : Controller {
        IRepository<Produit> _produitRepository;           //Possible de passer le modele à AddToCart

        public PanierController(IRepository<Produit> produitRepository) {
            _produitRepository = produitRepository;
        }

        [HttpPost]
        public ActionResult AddToCart(int id, int quantite) {
            string currentCartSerialized = HttpContext.Session.GetString("Cart");
            Dictionary<int, int> currentCart;
            if (currentCartSerialized.IsNullOrEmpty()) {
                currentCart = new Dictionary<int, int>();
                currentCart[id] = quantite;
            }
            else {
                currentCart = JsonConvert.DeserializeObject<Dictionary<int, int>>(currentCartSerialized);
                if (currentCart.ContainsKey(id)) {
                    currentCart[id] += quantite;
                }
                else {
                    currentCart[id] = quantite;
                }
            }
            currentCartSerialized = JsonConvert.SerializeObject(currentCart);
            HttpContext.Session.SetString("Cart", currentCartSerialized);
            return RedirectToAction("Index", "Tirelires");
        }
        // GET: PanierController
        public ActionResult SeeCart() {
            string currentCartSerialized = HttpContext.Session.GetString("Cart");
            Dictionary<int, int> currentCart;
            if (currentCartSerialized.IsNullOrEmpty()) {
                return RedirectToAction("Index", "Tirelires");
            }
            else {
                currentCart = JsonConvert.DeserializeObject<Dictionary<int, int>>(currentCartSerialized);
            }
            List<Produit> listeProduits = new List<Produit>();
            foreach (KeyValuePair<int, int> infosProduit in currentCart) {
                // TODO : performance : mettre en cache cette liste pour l'utiliser dans le CommandesController ?
                Produit produit = _produitRepository.GetById(infosProduit.Key);
                listeProduits.Add(produit);
                // Pour associer la quantité au produit
                ViewData[produit.Id.ToString()] = infosProduit.Value;
            }
            return View(listeProduits);
        }


        [HttpGet]
        public ActionResult Delete(int id) {
            string currentCartSerialized = HttpContext.Session.GetString("Cart");
            Dictionary<int, int> currentCart = JsonConvert.DeserializeObject<Dictionary<int, int>>(currentCartSerialized);
            currentCart.Remove(id);
            currentCartSerialized = JsonConvert.SerializeObject(currentCart);
            HttpContext.Session.SetString("Cart", currentCartSerialized);
            return RedirectToAction("SeeCart");
        }

        [HttpPost]
        public ActionResult Edit(int id, int quantite) {
            string currentCartSerialized = HttpContext.Session.GetString("Cart");
            Dictionary<int, int> currentCart = JsonConvert.DeserializeObject<Dictionary<int, int>>(currentCartSerialized);
            currentCart[id] = quantite;
            currentCartSerialized = JsonConvert.SerializeObject(currentCart);
            HttpContext.Session.SetString("Cart", currentCartSerialized);
            return RedirectToAction("SeeCart");
        }
    }
}
