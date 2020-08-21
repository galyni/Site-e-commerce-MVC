﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using SiteMVC.Repositories;

namespace SiteMVC.Controllers {
    public class TireliresController : Controller {
        IRepository<Produit> _repository;

        public TireliresController(IRepository<Produit> produitRepository) {
            _repository = produitRepository;
        }

        // GET: Tirelires
        // TODO : rendre invisibles les produits désactivés
        public IActionResult Index() {
            //var tireliresContext = _context.Produit.Include(p => p.IdCategorieNavigation).Include(p => p.IdCouleurNavigation).Include(p => p.IdFabricantNavigation).Include(p => p.IdFournisseurNavigation);
            var liste = _repository.GetList();
            foreach (Produit p in liste) {
                //_repository.GetPhoto(p);
            }
            return View(liste);
        }

        // TODO : rendre impossible la commande de produits dont le stock est éro (incohérence avec la règle du champ ?)
        public ActionResult Details(int id) {
            return View(_repository.GetById(id));
        }


        public ActionResult Create() {
            GetNavigationProperties();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Produit produit) {
            try {
                _repository.Create(produit);
                return RedirectToAction(nameof(Index));
            }
            catch {
                GetNavigationProperties();
                return View();
            }
        }

        public ActionResult Edit(int id) {
            GetNavigationProperties();
            return View(_repository.GetById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Produit produit) {
            try {
                _repository.Update(produit);
                return RedirectToAction(nameof(Index));
            }
            catch {
                GetNavigationProperties();
                return View(produit.Id);
            }
        }

        public ActionResult Delete(int id) {
            return View(_repository.GetById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Produit produit) {
            try {
                _repository.Delete(produit);
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
