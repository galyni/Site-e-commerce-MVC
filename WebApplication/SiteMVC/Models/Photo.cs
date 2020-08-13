using System;
using System.Collections.Generic;

namespace SiteMVC
{
    public partial class Photo
    {
        public int IdPhoto { get; set; }
        public int? IdProduit { get; set; }
        public string Image { get; set; }

        public virtual Produit IdProduitNavigation { get; set; }
    }
}
