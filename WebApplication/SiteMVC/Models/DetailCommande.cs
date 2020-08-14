using System;
using System.Collections.Generic;

namespace SiteMVC
{
    public partial class DetailCommande
    {
        public int Id { get; set; }
        public int IdCommande { get; set; }
        public int IdProduit { get; set; }
        public decimal PrixUnitaire { get; set; }
        public int Quantite { get; set; }

        public virtual Commande IdCommandeNavigation { get; set; }
        public virtual Produit IdProduitNavigation { get; set; }
    }
}
