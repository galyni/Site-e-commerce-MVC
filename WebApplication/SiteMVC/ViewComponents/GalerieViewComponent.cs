using Microsoft.AspNetCore.Mvc;
using SiteMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteMVC.ViewComponents {
    public class GalerieViewComponent : ViewComponent {
        IRepository<Produit> _produitRepository;
        public GalerieViewComponent(IRepository<Produit> produitRepository) {
            _produitRepository = produitRepository;
        }
        public Task<IViewComponentResult> InvokeAsync(int couleurId, int produitId) {
            var liste = _produitRepository.GetList().Where(p => p.IdCouleur == couleurId && p.Id != produitId).OrderBy(p=>p.Id).Take(4);
            return Task.FromResult<IViewComponentResult>(View("Galerie", liste));
        }
    }
}
