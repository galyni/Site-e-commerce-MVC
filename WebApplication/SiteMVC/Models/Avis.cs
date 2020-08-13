using System;
using System.Collections.Generic;

namespace SiteMVC
{
    public partial class Avis
    {
        public int IdAvis { get; set; }
        public int IdClient { get; set; }
        public int IdProduit { get; set; }
        public byte Note { get; set; }
        public DateTime Date { get; set; }
        public bool? Valide { get; set; }
        public string Moderateur { get; set; }
        public string Contenu { get; set; }

        public virtual Client IdClientNavigation { get; set; }
        public virtual Produit IdProduitNavigation { get; set; }
    }
}
