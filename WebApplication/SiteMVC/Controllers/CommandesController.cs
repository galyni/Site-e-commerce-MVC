using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using SiteMVC.Models;
using SiteMVC.Repositories;

namespace SiteMVC.Controllers {
    [Authorize]
    public class CommandesController : Controller {
        // TODO : harmoniser les noms et factoriser les controllers avec un générique
        private IRepository<Commande> _depotCommandes;
        private IRepository<DetailCommande> _depotDetail;
        private IRepository<Produit> _depotProduits;
        private IRepository<Client> _depotClients;
        private UserManager<WebsiteUser> _userManager;

        // TODO : segmenter ce controller, ou utiliser d'autres controllers existants, ou instancier les depots au moment de l'usage ?
        public CommandesController(IRepository<Commande> depotCommandes, IRepository<DetailCommande> depotDetail, IRepository<Produit> depotProduits, IRepository<Client> depotClients, UserManager<WebsiteUser> userManager) {
            _depotCommandes = depotCommandes;
            _depotDetail = depotDetail;
            _depotProduits = depotProduits;
            _depotClients = depotClients;
            _userManager = userManager;
        }
        //GET: CommandesController
        [Authorize(Roles = "Administrator")]
        public ActionResult Index() {
            return View("Index", _depotCommandes.GetList());
        }

        [Authorize(Roles = "User")]
        [Route("Commandes/IndexClient")]
        // TODO : tester Users n'ayant jamais passé de commandes -> gestion d'exceptions
        public async Task<ActionResult> IndexClientAsync() {
            WebsiteUser currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            string userMail = await _userManager.GetEmailAsync(currentUser);
            var liste = _depotCommandes.GetList().Where(c => c.IdClientNavigation.Mail == userMail);
            return View("IndexClient", liste);
        }

        [AllowAnonymous]
        public ActionResult TopProduits() {
            var top = _depotDetail.GetList()
                .GroupBy(d => d.IdProduit)
                .Select(group => new { Key = group.Key, Count = group.Count() })
                .OrderByDescending(group => group.Count)
                .Take(5)
                .ToList();
            List<Produit> liste = new List<Produit>();
            foreach (var item in top) {
                liste.Add(_depotProduits.GetById(item.Key));
                ViewData[item.Key.ToString()] = item.Count;
            }
            return View(liste);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult TopClients() {
            var top = _depotCommandes.GetList()
                .GroupBy(c => c.IdClient)
                .Select(group => new { Key = group.Key, Total = group.Sum(ligne => ligne.Total) })
                .OrderByDescending(group => group.Total)
                .Take(5)
                .ToList();
            List<Client> liste = new List<Client>();
            foreach (var item in top) {
                liste.Add(_depotClients.GetById(item.Key));
                ViewData[item.Key.ToString()] = item.Total;
            }
            return View(liste);
        }

        // GET: CommandesController/Details/5
        [Route("Commandes/Details")]
        //TODO : supprimer vue Commandes/Details.cshtml
        public ActionResult DetailsCommande(int idCommande) {
            var liste = _depotDetail.GetList().Where(d => d.IdCommande == idCommande);
            ViewBag.CommandeId = idCommande;
            return View(liste);
        }

        // GET: CommandesController/Create
        [Route("Commandes/Validate/{total}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> ValidateAsync(decimal total) {
            // Test si le User existe en tant que client, par l'adresse mail (requise comme unique)
            //TODO : unicité de l'adresse mail
            WebsiteUser currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            string userMail = await _userManager.GetEmailAsync(currentUser);
            Client client = _depotClients.GetList().Where(c => c.Mail == userMail).SingleOrDefault();

            //Si nouveau client, on envoie au controller, en stockant le total, dont on aura besoin
            if (client == null) {
                string totalSerialized = JsonConvert.SerializeObject(total);
                HttpContext.Session.SetString("total", totalSerialized);
                return RedirectToAction("Create", "Client", new { mail = userMail });
            }
            else if (!client.Actif) {
                return RedirectToAction("Warning","Client");
            }
            else {
                Commande commande = new Commande() {
                    IdClient = client.Id,
                    DateCommande = DateTime.Now,
                    IdStatut = 1,
                    Total = total,
                    IdAdresse = client.IdAdresse
                };
                // TODO : gestion d'exception (ici et dans toute cette méthode)
                commande = _depotCommandes.Create(commande);
                //TODO : factoriser ça... (ou TempData ?). Mais peut-être est-on obligés de repasser par là.
                string currentCartSerialized = HttpContext.Session.GetString("Cart");
                List<KeyValuePair<int, int>> currentCart = JsonConvert.DeserializeObject<List<KeyValuePair<int, int>>>(currentCartSerialized);
                foreach (KeyValuePair<int, int> infosProduit in currentCart) {
                    Produit produit = _depotProduits.GetById(infosProduit.Key);
                    DetailCommande detailCommande = new DetailCommande() {
                        IdCommande = commande.Id,
                        IdProduit = produit.Id,
                        PrixUnitaire = produit.PrixUnitaire,
                        Quantite = infosProduit.Value
                    };
                    _depotDetail.Create(detailCommande);
                }
            }
            // Pour vider le panier après la commande. Ou mieux vaut SetString("Cart", """) ?
            HttpContext.Session.Remove("Cart");
            return RedirectToAction("Index", "Tirelires");
        }

        //TODO : découper Validate en plusieurs fonctions, dont Create ?

        // POST: CommandesController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(int id) {
        //    try {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch {
        //        return View();
        //    }
        //}

        //POST: CommandesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        // TODO : rôle supplémentaire qui aurait la responsabilité sur l'avancée de la commande
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(Commande commande) {
            try {
                if (commande.IdStatut == 4) {
                    commande.DateLivraison = DateTime.Now;
                }
                _depotCommandes.Update(commande);
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }

        // GET: CommandesController/Delete/5
        //public ActionResult Delete(int id) {
        //    return View();
        //}

        // POST: CommandesController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(Commande commande) {
        //    try {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch {
        //        return View();
        //    }
        //}
    }
}
