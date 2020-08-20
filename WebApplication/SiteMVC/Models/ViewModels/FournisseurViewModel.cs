using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteMVC.Models.ViewModels {
    public class FournisseurViewModel {
        public string Nom { get; set; }
        public string Siret { get; set; }
        public string Telephone { get; set; }
        public string Mail { get; set; }
        public int Numero { get; set; }
        public string Rue { get; set; }
        public string CodePostal { get; set; }
        public string Ville { get; set; }
        public string Pays { get; set; }
    }
}
