using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;

namespace SiteMVC
{
    public partial class Adresse
    {
        public Adresse()
        {
            Client = new HashSet<Client>();
            Commande = new HashSet<Commande>();
            Fournisseur = new HashSet<Fournisseur>();
        }

        public int Id { get; set; }
        public int Numero { get; set; }
        public string Rue { get; set; }
        public string CodePostal { get; set; }
        public string Ville { get; set; }
        public string Pays { get; set; }

        public virtual ICollection<Client> Client { get; set; }
        public virtual ICollection<Commande> Commande { get; set; }
        public virtual ICollection<Fournisseur> Fournisseur { get; set; }

        public override string ToString() {
            return $"{Numero} {Rue} {CodePostal} {Ville} {Pays}";
        }
    }
}
