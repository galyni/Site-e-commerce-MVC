using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SiteMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteMVC.ViewComponents {
    public class AvisViewComponent : ViewComponent {
        IRepository<Avis> _avisRepository;
        public AvisViewComponent(IRepository<Avis> avisRepository) {
            _avisRepository = avisRepository;
        }
        public Task<IViewComponentResult> InvokeAsync(int produitId) {
            var liste = _avisRepository.GetList().Where(a=>a.Valide == true && a.IdProduit == produitId).OrderBy(a => a.Id).Take(4);
            return Task.FromResult<IViewComponentResult>(View("Avis", liste));
        }
    }
}
