using System;
using System.Collections.Generic;

namespace SiteMVC
{
    public partial class Produit
    {
        public Produit()
        {
            Avis = new HashSet<Avis>();
            DetailCommande = new HashSet<DetailCommande>();
            Photo = new HashSet<Photo>();
        }

        public int Id { get; set; }
        public string Nom { get; set; }
        public decimal PrixUnitaire { get; set; }
        public decimal? Hauteur { get; set; }
        public decimal? Longueur { get; set; }
        public decimal? Largeur { get; set; }
        public decimal Poids { get; set; }
        public int? Capacite { get; set; }
        public string Description { get; set; }
        public int? IdCouleur { get; set; }
        public int? IdFabricant { get; set; }
        public int IdFournisseur { get; set; }
        public int IdCategorie { get; set; }
        public int Stock { get; set; }
        public bool Statut { get; set; }

        public virtual Categorie IdCategorieNavigation { get; set; }
        public virtual Couleur IdCouleurNavigation { get; set; }
        public virtual Fabricant IdFabricantNavigation { get; set; }
        public virtual Fournisseur IdFournisseurNavigation { get; set; }
        public virtual ICollection<Avis> Avis { get; set; }
        public virtual ICollection<DetailCommande> DetailCommande { get; set; }
        public virtual ICollection<Photo> Photo { get; set; }
    }
}
