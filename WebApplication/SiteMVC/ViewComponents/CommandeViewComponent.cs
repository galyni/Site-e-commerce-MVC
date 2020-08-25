using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SiteMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteMVC.ViewComponents {
    public class CommandeViewComponent : ViewComponent {
        IRepository<Commande> _commandeRepository;
        // TODO : repenser l'organisation du view component ?
        public CommandeViewComponent(IRepository<Commande> commandeRepository) {
            _commandeRepository = commandeRepository;
        }
        public Task<IViewComponentResult> InvokeAsync(int commandeId) {
            ViewBag.IdStatut = new Repository<StatutCommande>().GetList().Select(
          c => new SelectListItem { Text = c.Nom, Value = c.Id.ToString() }
                );
            return Task.FromResult<IViewComponentResult>(View("Commande", _commandeRepository.GetById(commandeId)));
        }
    }
}
