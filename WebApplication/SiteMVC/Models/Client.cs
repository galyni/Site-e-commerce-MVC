using System;
using System.Collections.Generic;

namespace SiteMVC
{
    public partial class Client
    {
        public Client()
        {
            Avis = new HashSet<Avis>();
            Commande = new HashSet<Commande>();
        }

        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public bool Actif { get; set; }
        public int IdAdresse { get; set; }
        public string mail { get; set; }
        public virtual Adresse IdAdresseNavigation { get; set; }
        public virtual ICollection<Avis> Avis { get; set; }
        public virtual ICollection<Commande> Commande { get; set; }

    }
}
