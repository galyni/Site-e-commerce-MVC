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
        // TODO En faire une propriété de navigation ? Mettre le mail à la place ?
        public string IdUser { get;set; }

        public virtual Adresse IdAdresseNavigation { get; set; }
        public virtual ICollection<Avis> Avis { get; set; }
        public virtual ICollection<Commande> Commande { get; set; }

    }
}
