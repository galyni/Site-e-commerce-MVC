using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SiteMVC.Models.ViewModels;
using SiteMVC.Repositories;

namespace SiteMVC.Controllers {
    [Authorize(Roles ="Administrator")]
    public class FournisseursController : Controller {
        IRepository<Fournisseur> _repositoryFournisseur;
        IRepository<Adresse> _repositoryAdresse;

        public FournisseursController(IRepository<Fournisseur> repositoryFournisseur, IRepository<Adresse> repositoryAdresse) {
            _repositoryFournisseur = repositoryFournisseur;
            _repositoryAdresse = repositoryAdresse;

        }
        // GET: Fournisseurs
        public ActionResult Index() {
            var liste = _repositoryFournisseur.GetList();
            return View(liste);
        }

        // GET: Fournisseurs/Create
        public ActionResult Create() {
            return View();
        }

        // POST: Fournisseurs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FournisseurViewModel data) {
            try {
                Adresse adresse = _repositoryAdresse.Create(new Adresse() { Numero = data.Numero, Rue = data.Rue, CodePostal = data.CodePostal, Ville = data.Ville, Pays = data.Pays });
                Fournisseur fournisseur = new Fournisseur() { Nom = data.Nom, Siret = data.Siret, Telephone = data.Telephone, Mail = data.Mail, IdAdresse=adresse.Id };
                _repositoryFournisseur.Create(fournisseur);
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }

        // GET: Fournisseurs/Edit/5
        public ActionResult Edit(int id) {
            Fournisseur Fournisseur = _repositoryFournisseur.GetById(id);
            ViewBag.IdAdresse = new Repository<Adresse>().GetList().Select(
          a => new SelectListItem { Text = String.Concat(a.Numero, a.Rue, a.CodePostal, a.Ville, a.Pays), Value = a.Id.ToString() }
                );
            return View(Fournisseur);
        }

        // POST: Fournisseurs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Fournisseur fournisseur) {
            try {
                _repositoryFournisseur.Update(fournisseur);
                return RedirectToAction(nameof(Index));
            }
            catch {
                ViewBag.IdAdresse = new Repository<Adresse>().GetList().Select(
          a => new SelectListItem { Text = String.Concat(a.Numero, a.Rue, a.CodePostal, a.Ville, a.Pays), Value = a.Id.ToString() }
                );
                return View(fournisseur.Id);
            }
        }

        // GET: Fournisseurs/Delete/5
        public ActionResult Delete(int id) {
            var item = _repositoryFournisseur.GetById(id);
            return View(item);
        }

        // POST: Fournisseurs/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Fournisseur fournisseur) {
            try {
                _repositoryFournisseur.Delete(fournisseur);
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }
    }
}
