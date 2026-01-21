using Erp_Api.Models.Entity.Tables.Entitees;
using Erp_Api.Models.Entity.Tables.Jointures;
using Microsoft.EntityFrameworkCore;

namespace Erp_Api.Models.Entity
{
    public partial class ErpBdContext : DbContext
    {
        public ErpBdContext()
        {
        }
        public ErpBdContext(DbContextOptions<ErpBdContext> options)
            : base(options)
        {
        }
        public DbSet<Employe> Employes { get; set; }
        public DbSet<Entite> Entites { get; set; }
        public DbSet<EmployeEntite> EmployeEntites { get; set; }
        public DbSet<Adresse> Adresses { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<A_Pour_Role> A_Pour_Roles { get; set; }
        public DbSet<Candidature> Candidatures { get; set; }
        public DbSet<Offre> Offres { get; set; }
        public DbSet<TypeContrat> TypeContrats { get; set; }
        public DbSet<Entretien> Entretiens { get; set; }
        public DbSet<TypeEntretien> TypeEntretiens { get; set; }
        public DbSet<Plateforme> Plateformes { get; set; }
        public DbSet<Projet> Projets { get; set; }
        public DbSet<Tache> Taches { get; set; }
        public DbSet<Materiel> Materiels { get; set; }
        public DbSet<FeuilleTemps> FeuillesTemps { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("erp");

            modelBuilder.Entity<Employe>()
                .HasOne(e => e.Adresse)
                .WithOne(a => a.Employe)
                .HasForeignKey<Adresse>(a => a.EmployeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Entite>()
                .HasOne(e => e.Adresse)
                .WithOne(a => a.Entite)
                .HasForeignKey<Adresse>(a => a.EntrepriseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EmployeEntite>()
                .HasKey(ee => new { ee.EmployeId, ee.Entite });

            modelBuilder.Entity<EmployeEntite>()
                .HasOne(ee => ee.Employe)
                .WithMany(e => e.EmployeEntites)
                .HasForeignKey(ee => ee.EmployeId)
                .OnDelete(DeleteBehavior.Cascade);
                
            modelBuilder.Entity<EmployeEntite>()
                .HasOne(ee => ee.Entite)
                .WithMany(e => e.EmployeEntites)
                .HasForeignKey(ee => ee.EntiteId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<A_Pour_Role>()
                 .HasKey(apr => new { apr.EmployeId, apr.RoleId });

            modelBuilder.Entity<A_Pour_Role>()
                .HasOne(apr => apr.Employe)
                .WithMany(e => e.Employeroles)
                .HasForeignKey(apr => apr.EmployeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<A_Pour_Role>()
                .HasOne(apr => apr.Role)
                .WithMany(r => r.EmployeRoles)
                .HasForeignKey(apr => apr.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            // Offre -> TypeContrat
            modelBuilder.Entity<Offre>()
                .HasOne(o => o.TypeContrat)
                .WithMany(tc => tc.Offres)
                .HasForeignKey(o => o.TypeContratId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Entretien>()
                .HasOne(e => e.TypeEntretien)
                .WithMany()
                .HasForeignKey(e => e.TypeEntretienId)
                .OnDelete(DeleteBehavior.Restrict);

            // Candidature -> Offre
            modelBuilder.Entity<Candidature>()
                .HasOne(c => c.Offre)
                .WithMany(o => o.Candidatures)
                .HasForeignKey(c => c.OffreEmploiId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Projet>()
                .HasOne(p => p.EntiteRealisatrice)
                .WithMany(e => e.ProjetsRealises)
                .HasForeignKey(p => p.EntrepriseRealisatriceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Projet>()
                .HasOne(p => p.EntiteCliente)
                .WithMany(e => e.ProjetsCommandes)
                .HasForeignKey(p => p.EntrepriseClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tache>()
                .HasOne(t => t.TacheParente)
                .WithMany(t => t.SousTaches)
                .HasForeignKey(t => t.TacheParenteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tache>()
                .HasOne(t => t.Projet)
                .WithMany(p => p.Taches)    
                .HasForeignKey(t => t.ProjetId)
                .OnDelete(DeleteBehavior.Cascade);

            OnModelCreatingPartial(modelBuilder);

        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
