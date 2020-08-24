using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SiteMVC.Models;
using SiteMVC.Models.ViewModels;
using SiteMVC.Repositories;

namespace SiteMVC.Controllers {
    [Authorize(Roles = "Moderator, User")]
    public class AvisController : Controller {
        private IRepository<Avis> _avisRepository;
        private IRepository<Produit> _produitRepository;
        private IRepository<DetailCommande> _detailRepository;

        //private IRepository<Client> _clientRepository;
        //private UserManager<WebsiteUser> _userManager;

        //TODO : empecher de proposer un avis lorsque c'est déjà fait, mais proposer de le modifier
        public AvisController(IRepository<Avis> avisRepository, IRepository<Produit> produitRepository, IRepository<DetailCommande> detailRepository/*, IRepository<Client> clientRepository, UserManager<WebsiteUser> userManager*/) {
            _avisRepository = avisRepository;
            _produitRepository = produitRepository;
            _detailRepository = detailRepository;

            //_clientRepository = clientRepository;
            //_userManager = userManager;
        }
        // GET: Couleurs

        [Authorize(Roles = "Moderator")]
        public ActionResult Index() {
            var liste = _avisRepository.GetList();
            return View(liste);
        }

        [Authorize(Roles = "Moderator")]
        public ActionResult AttenteModeration() {
            var liste = _avisRepository.GetList().Where(a => a.Valide == null);
            return View(liste);
        }

        [Authorize(Roles = "Moderator")]
        public ActionResult Review(int id) {
            Avis avis = _avisRepository.GetById(id);
            return View(avis);
        }

        [Authorize(Roles = "Moderator")]
        public ActionResult Validate(int id, bool validate) {
            Avis avis = _avisRepository.GetById(id);
            if (validate) {
                avis.Valide = true;
                avis.Moderateur = User.Identity.Name;
                _avisRepository.Update(avis);
            }
            else {
                _avisRepository.Delete(avis);
            }
            return RedirectToAction("AttenteModeration");
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        // TODO : redirection si l'utilisateur a déjà donné son avis
        public ActionResult Create(int idProduit, int idDetail) {
            var model = new AvisViewModel() { produit = _produitRepository.GetById(idProduit) };
            //WebsiteUser currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            //string userMail = await _userManager.GetEmailAsync(currentUser);
            ViewBag.IdClient = _detailRepository.GetById(idDetail).IdCommandeNavigation.IdClient;
            return View(model);
        }

        // POST: Couleurs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public ActionResult Create(AvisViewModel avisViewModel) {
            try {
                Avis avis = new Avis {
                    IdClient = avisViewModel.IdClient,
                    IdProduit = avisViewModel.IdProduit,
                    Note = avisViewModel.Note,
                    Date = DateTime.Now,
                    Contenu = avisViewModel.Contenu
                };
                _avisRepository.Create(avis);
                return RedirectToAction("Index", "Tirelires");
            }
            catch {
                return View();
            }
        }

        // GET: Couleurs/Edit/5
        //public ActionResult Edit(int id) {
        //    Couleur couleur = _avisRepository.GetById(id);
        //    return View(couleur);
        //}

        // POST: Couleurs/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(Couleur couleur) {
        //    try {
        //        _avisRepository.Update(couleur);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch {
        //        return View();
        //    }
        //}

        // GET: Couleurs/Delete/5
        //    public ActionResult Delete(int id) {
        //        var item = _avisRepository.GetById(id);
        //        return View(item);
        //    }

        //    // POST: Couleurs/Delete/5
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult Delete(Avis avis) {
        //        try {
        //            _avisRepository.Delete(avis);
        //            return RedirectToAction(nameof(Index));
        //        }
        //        catch {
        //            return View();
        //        }
        //    }
    }
}
