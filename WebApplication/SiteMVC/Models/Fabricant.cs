using System;
using System.Collections.Generic;

namespace SiteMVC
{
    public partial class Fabricant
    {
        public Fabricant()
        {
            Produit = new HashSet<Produit>();
        }

        public int IdFabricant { get; set; }
        public string Nom { get; set; }

        public virtual ICollection<Produit> Produit { get; set; }
    }
}
