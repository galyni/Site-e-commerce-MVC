using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SiteMVC.Models.ViewModels;
using SiteMVC.Repositories;

namespace SiteMVC.Controllers {
    [Authorize]
    public class ClientController : Controller {
        private IRepository<Client> _depotClient;
        private IRepository<Adresse> _repositoryAdresse;
        public ClientController(IRepository<Client> depotClient, IRepository<Adresse> repositoryAdresse) {
            _depotClient = depotClient;
            _repositoryAdresse = repositoryAdresse;
        }
        // GET: ClientController
        [Authorize(Roles ="Administrator")]
        public ActionResult Index() {
            return View(_depotClient.GetList());
        }

        [Authorize(Roles ="Administrator")]
        public ActionResult Desactivate(int id) {
            Client client = _depotClient.GetById(id);
            client.Actif = false;
            _depotClient.Update(client);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Reactivate(int id) {
            Client client = _depotClient.GetById(id);
            client.Actif = true;
            _depotClient.Update(client);
            return RedirectToAction("Index");
        }

        public ActionResult Warning() {
            return View();
        }

        // GET: ClientController/Details/5
        public ActionResult Details(int id) {
            return View();
        }

        // GET: ClientController/Create
        public ActionResult Create(string mail) {
            // Ou TempData ?
            ViewBag.mail = mail;
            return View();
        }

        // POST: ClientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClientViewModel clientViewModel) {
            //try {
                Adresse adresse = _repositoryAdresse.Create(new Adresse() { Numero = clientViewModel.Numero, Rue = clientViewModel.Rue, CodePostal = clientViewModel.CodePostal, Ville = clientViewModel.Ville, Pays = clientViewModel.Pays });
                Client client = new Client() { Nom = clientViewModel.Nom, Prenom = clientViewModel.Prenom, Actif = clientViewModel.Actif, Mail = clientViewModel.Mail, IdAdresse = adresse.Id };
                _depotClient.Create(client);
                decimal total = JsonConvert.DeserializeObject<decimal>(HttpContext.Session.GetString("total"));
                // Acceptable parce que le seul accès est par une commande en cours
                return RedirectToAction("Validate", "Commandes", new { id = total });
            //}
            //catch {
                //return View();
            //}
        }

        // GET: ClientController/Edit/5
        public ActionResult Edit(int id) {
            return View();
        }

        // POST: ClientController/Edit/5
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
    }
}
