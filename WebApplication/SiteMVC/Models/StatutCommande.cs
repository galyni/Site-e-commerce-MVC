using System;
using System.Collections.Generic;

namespace SiteMVC
{
    public partial class StatutCommande
    {
        public StatutCommande()
        {
            Commande = new HashSet<Commande>();
        }

        public int Id { get; set; }
        public string Nom { get; set; }

        public virtual ICollection<Commande> Commande { get; set; }
    }
}
