using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteMVC.Models.ViewModels {
    public class AvisViewModel {
        public Produit produit;
        public int Id { get; set; }
        public int IdClient { get; set; }
        public int IdProduit { get; set; }
        public byte Note { get; set; }
        public DateTime Date { get; set; }
        public bool? Valide { get; set; }
        public string Moderateur { get; set; }
        public string Contenu { get; set; }
    }
}
