using System;
using System.Collections.Generic;

namespace SiteMVC
{
    public partial class Commande
    {
        public Commande()
        {
            DetailCommande = new HashSet<DetailCommande>();
        }

        public int Id { get; set; }
        public int IdClient { get; set; }
        public DateTime DateCommande { get; set; }
        public DateTime? DateLivraison { get; set; }
        public int IdStatut { get; set; }
        public decimal Total { get; set; }
        public int IdAdresse { get; set; }

        public virtual Adresse IdAdresseNavigation { get; set; }
        public virtual Client IdClientNavigation { get; set; }
        public virtual StatutCommande IdStatutNavigation { get; set; }
        public virtual ICollection<DetailCommande> DetailCommande { get; set; }
    }
}
