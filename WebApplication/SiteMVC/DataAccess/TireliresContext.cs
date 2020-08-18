using System;
using System.IO;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using SiteMVC.Models;

namespace SiteMVC
{
    public partial class TireliresContext : IdentityDbContext<WebsiteUser>
    {
        public TireliresContext()
        {
        }

        public TireliresContext(DbContextOptions<TireliresContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Adresse> Adresse { get; set; }
        public virtual DbSet<Avis> Avis { get; set; }
        public virtual DbSet<Categorie> Categorie { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Commande> Commande { get; set; }
        public virtual DbSet<Couleur> Couleur { get; set; }
        public virtual DbSet<DetailCommande> DetailCommande { get; set; }
        public virtual DbSet<Fabricant> Fabricant { get; set; }
        public virtual DbSet<Fournisseur> Fournisseur { get; set; }
        public virtual DbSet<Photo> Photo { get; set; }
        public virtual DbSet<Produit> Produit { get; set; }
        public virtual DbSet<StatutCommande> StatutCommande { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adresse>(entity =>
            {
                entity.Property(e => e.CodePostal)
                    .IsRequired()
                    .HasColumnName("codePostal")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Numero).HasColumnName("numero");

                entity.Property(e => e.Pays).HasMaxLength(50);

                entity.Property(e => e.Rue)
                    .IsRequired()
                    .HasColumnName("rue")
                    .HasMaxLength(50);

                entity.Property(e => e.Ville)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Avis>(entity =>
            {
                entity.Property(e => e.Moderateur).HasMaxLength(50);

                entity.Property(e => e.Note).HasColumnName("note");

                entity.HasOne(d => d.IdClientNavigation)
                    .WithMany(p => p.Avis)
                    .HasForeignKey(d => d.IdClient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Avis_Client");

                entity.HasOne(d => d.IdProduitNavigation)
                    .WithMany(p => p.Avis)
                    .HasForeignKey(d => d.IdProduit)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Avis_Produit");
            });

            modelBuilder.Entity<Categorie>(entity =>
            {
                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasColumnName("Nom")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Prenom)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdAdresseNavigation)
                    .WithMany(p => p.Client)
                    .HasForeignKey(d => d.IdAdresse)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Client_Adresse");
            });

            modelBuilder.Entity<Commande>(entity =>
            {
                entity.Property(e => e.DateLivraison).HasColumnType("date");

                entity.Property(e => e.Total).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.IdAdresseNavigation)
                    .WithMany(p => p.Commande)
                    .HasForeignKey(d => d.IdAdresse)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Commande_Adresse");

                entity.HasOne(d => d.IdClientNavigation)
                    .WithMany(p => p.Commande)
                    .HasForeignKey(d => d.IdClient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Commande_Client");

                entity.HasOne(d => d.IdStatutNavigation)
                    .WithMany(p => p.Commande)
                    .HasForeignKey(d => d.IdStatut)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Commande_StatutCommande");
            });

            modelBuilder.Entity<Couleur>(entity =>
            {
                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasColumnName("Nom")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<DetailCommande>(entity =>
            {
                entity.Property(e => e.PrixUnitaire).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.IdCommandeNavigation)
                    .WithMany(p => p.DetailCommande)
                    .HasForeignKey(d => d.IdCommande)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DetailCommande_Commande");

                entity.HasOne(d => d.IdProduitNavigation)
                    .WithMany(p => p.DetailCommande)
                    .HasForeignKey(d => d.IdProduit)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DetailCommande_Produit");
            });

            modelBuilder.Entity<Fabricant>(entity =>
            {
                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Fournisseur>(entity =>
            {
                entity.Property(e => e.Mail)
                    .HasColumnName("mail")
                    .HasMaxLength(50);

                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Siret)
                    .IsRequired()
                    .HasColumnName("SIRET")
                    .HasMaxLength(50);

                entity.Property(e => e.Telephone).HasMaxLength(50);

                entity.HasOne(d => d.IdAdresseNavigation)
                    .WithMany(p => p.Fournisseur)
                    .HasForeignKey(d => d.IdAdresse)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Fournisseur_Adresse");
            });

            modelBuilder.Entity<Photo>(entity =>
            {
                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.IdProduitNavigation)
                    .WithMany(p => p.Photo)
                    .HasForeignKey(d => d.IdProduit)
                    .HasConstraintName("FK_Photo_Produit");
            });

            modelBuilder.Entity<Produit>(entity =>
            {
                entity.Property(e => e.Hauteur).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Largeur).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Longueur).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Poids).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PrixUnitaire).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.IdCategorieNavigation)
                    .WithMany(p => p.Produit)
                    .HasForeignKey(d => d.IdCategorie)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Produit_Categorie");

                entity.HasOne(d => d.IdCouleurNavigation)
                    .WithMany(p => p.Produit)
                    .HasForeignKey(d => d.IdCouleur)
                    .HasConstraintName("FK_Produit_Couleur");

                entity.HasOne(d => d.IdFabricantNavigation)
                    .WithMany(p => p.Produit)
                    .HasForeignKey(d => d.IdFabricant)
                    .HasConstraintName("FK_Produit_Fabricant");

                entity.HasOne(d => d.IdFournisseurNavigation)
                    .WithMany(p => p.Produit)
                    .HasForeignKey(d => d.IdFournisseur)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Produit_Fournisseur");
            });

            modelBuilder.Entity<StatutCommande>(entity =>
            {
                entity.Property(e => e.Nom).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
