using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Newtonsoft.Json;
using SiteMVC.Repositories;

namespace SiteMVC.Controllers {
    public class TireliresController : Controller {
        IRepository<Produit> _produitRepository;

        public TireliresController(IRepository<Produit> produitRepository) {
            _produitRepository = produitRepository;
        }

        // GET: Tirelires
        // TODO : rendre invisibles les produits désactivés
        public IActionResult Index() {
            //var tireliresContext = _context.Produit.Include(p => p.IdCategorieNavigation).Include(p => p.IdCouleurNavigation).Include(p => p.IdFabricantNavigation).Include(p => p.IdFournisseurNavigation);
            var liste = _produitRepository.GetList().Where(p=>p.Statut);
            return View(liste);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult DeactivatedList() {
            var liste = _produitRepository.GetList().Where(p => !p.Statut);
            return View(liste);
        }

        public ActionResult Details(int id) {
            string currentCartSerialized = HttpContext.Session.GetString("Cart");
            if (!currentCartSerialized.IsNullOrEmpty()) {
                var currentCart = JsonConvert.DeserializeObject<Dictionary<int, int>>(currentCartSerialized);
                if (currentCart.ContainsKey(id)) {
                    ViewBag.QuantiteActuelle = currentCart[id];
                }
            }
            return View(_produitRepository.GetById(id));
        }


        [Authorize(Roles = "Administrator")]
        public ActionResult Create() {
            GetNavigationProperties();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create(Produit produit) {
            try {
                _produitRepository.Create(produit);
                return RedirectToAction(nameof(Index));
            }
            catch {
                GetNavigationProperties();
                return View();
            }
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id) {
            GetNavigationProperties();
            return View(_produitRepository.GetById(id));
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Deactivate(int id) {
           Produit produit = _produitRepository.GetById(id);
            produit.Statut = false;
            _produitRepository.Update(produit);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Reactivate(int id) {
            Produit produit = _produitRepository.GetById(id);
            produit.Statut = true;
            _produitRepository.Update(produit);
            return RedirectToAction("DeactivatedList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(Produit produit) {
            try {
                _produitRepository.Update(produit);
                return RedirectToAction(nameof(Index));
            }
            catch {
                GetNavigationProperties();
                return View(produit.Id);
            }
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id) {
            return View(_produitRepository.GetById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(Produit produit) {
            try {
                _produitRepository.Delete(produit);
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }


        private void GetNavigationProperties() {
            ViewBag.IdCouleur = new Repository<Couleur>().GetList().Select(
          c => new SelectListItem { Text = c.Nom, Value = c.Id.ToString() }
                );
            ViewBag.IdFabricant = new Repository<Fabricant>().GetList().Select(
            c => new SelectListItem { Text = c.Nom, Value = c.Id.ToString() }
                );
            ViewBag.IdFournisseur = new Repository<Fournisseur>().GetList().Select(
            c => new SelectListItem { Text = c.Nom, Value = c.Id.ToString() }
                );
            ViewBag.IdCategorie = new Repository<Categorie>().GetList().Select(
            c => new SelectListItem { Text = c.Nom, Value = c.Id.ToString() }
                );
        }
    }
}
