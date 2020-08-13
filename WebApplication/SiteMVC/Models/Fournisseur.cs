using System;
using System.Collections.Generic;

namespace SiteMVC
{
    public partial class Fournisseur
    {
        public Fournisseur()
        {
            Produit = new HashSet<Produit>();
        }

        public int IdFournisseur { get; set; }
        public string Nom { get; set; }
        public string Siret { get; set; }
        public string Telephone { get; set; }
        public string Mail { get; set; }
        public int IdAdresse { get; set; }

        public virtual Adresse IdAdresseNavigation { get; set; }
        public virtual ICollection<Produit> Produit { get; set; }
    }
}
