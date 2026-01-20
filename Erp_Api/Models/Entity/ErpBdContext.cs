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
        public DbSet<Entreprise> Entreprises { get; set; }
        public DbSet<EmployeEntreprise> EmployeEntreprises { get; set; }
        public DbSet<Adresse> Adresses { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<A_Pour_Role> A_Pour_Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("erp");

            modelBuilder.Entity<Employe>()
                .HasOne(e => e.Adresse)
                .WithOne(a => a.Employe)
                .HasForeignKey<Adresse>(a => a.EmployeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Entreprise>()
                .HasOne(e => e.Adresse)
                .WithOne(a => a.Entreprise)
                .HasForeignKey<Adresse>(a => a.EntrepriseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EmployeEntreprise>()
                .HasKey(ee => new { ee.EmployeId, ee.EntrepriseId });

            modelBuilder.Entity<EmployeEntreprise>()
                .HasOne(ee => ee.Employe)
                .WithMany(e => e.EmployeEntreprises)
                .HasForeignKey(ee => ee.EmployeId)
                .OnDelete(DeleteBehavior.Cascade);
                
            modelBuilder.Entity<EmployeEntreprise>()
                .HasOne(ee => ee.Entreprise)
                .WithMany(e => e.EmployeEntreprises)
                .HasForeignKey(ee => ee.EntrepriseId)
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

            OnModelCreatingPartial(modelBuilder);

        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
