using System;
using System.Collections.Generic;

namespace SiteMVC
{
    public partial class Photo
    {
        public int Id { get; set; }
        public int? IdProduit { get; set; }
        public byte[] Image { get; set; }

        public virtual Produit IdProduitNavigation { get; set; }
    }
}
