using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SiteMVC
{
    public partial class TireliresContext : DbContext
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-E1G8NKK;Initial Catalog=Tirelires;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adresse>(entity =>
            {
                entity.HasKey(e => e.IdAdresse);

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
                entity.HasKey(e => e.IdAvis);

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
                entity.HasKey(e => e.IdCategorie);

                entity.Property(e => e.Categorie1)
                    .IsRequired()
                    .HasColumnName("Categorie")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.IdClient);

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
                entity.HasKey(e => e.IdCommande);

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
                entity.HasKey(e => e.IdCouleur);

                entity.Property(e => e.Couleur1)
                    .IsRequired()
                    .HasColumnName("Couleur")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<DetailCommande>(entity =>
            {
                entity.HasKey(e => new { e.IdCommande, e.IdProduit });

                entity.Property(e => e.IdCommande).ValueGeneratedOnAdd();

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
                entity.HasKey(e => e.IdFabricant);

                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Fournisseur>(entity =>
            {
                entity.HasKey(e => e.IdFournisseur);

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
                entity.HasKey(e => e.IdPhoto)
                    .HasName("PK_Photo_1");

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
                entity.HasKey(e => e.IdProduit);

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
                entity.HasKey(e => e.IdStatut);

                entity.Property(e => e.Nom).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
