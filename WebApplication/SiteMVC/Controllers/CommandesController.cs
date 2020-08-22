using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SiteMVC.Models;
using SiteMVC.Repositories;

namespace SiteMVC.Controllers {
    public class CommandesController : Controller {
        // TODO : harmoniser les noms et factoriser les controllers avec un générique
        private IRepository<Commande> _depotCommandes;
        private IRepository<DetailCommande> _depotDetail;
        private IRepository<Produit> _depotProduits;
        private IRepository<Client> _depotClients;
        private UserManager<WebsiteUser> _userManager;

        // TODO : segmenter ce controller, ou utiliser d'autres controllers existants
        public CommandesController(IRepository<Commande> depotCommandes, IRepository<DetailCommande> depotDetail, IRepository<Produit> depotProduits, IRepository<Client> depotClients, UserManager<WebsiteUser> userManager) {
            _depotCommandes = depotCommandes;
            _depotDetail = depotDetail;
            _depotProduits = depotProduits;
            _depotClients = depotClients;
            _userManager = userManager;
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
        [Route("Commandes/Validate/{total}")]
        public async Task<ActionResult> ValidateAsync(decimal total) 
            {
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
            else {
                Commande commande = new Commande() { 
                    IdClient = client.Id, 
                    DateCommande = DateTime.Now, 
                    IdStatut = 1, Total = total, 
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
                        IdProduit=produit.Id,
                        PrixUnitaire=produit.PrixUnitaire,
                        Quantite=infosProduit.Value
                    };
                    _depotDetail.Create(detailCommande);
                }
            }
            // Pour vider le panier après la commande. Ou mieux vaut SetString("Cart", """) ?
            HttpContext.Session.Remove("Cart");
            return RedirectToAction("Index", "Tirelires");
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
        public ActionResult Edit(int id, Commande commande) {
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
        public ActionResult Delete(Commande commande) {
            try {
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }
    }
}
