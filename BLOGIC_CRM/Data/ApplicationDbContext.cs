using Microsoft.EntityFrameworkCore;
using BLOGIC_CRM.Models;


namespace BLOGIC_CRM.Data

{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Smlouva> Smlouvy { get; set; }
        public DbSet<Klient> Klienti { get; set; }
        public DbSet<Poradce> Poradci { get; set; }
        public DbSet<SmlouvaPoradce> SmlouvaPoradci { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //složený klíč SmlouvaId,PoradceId
            modelBuilder.Entity<SmlouvaPoradce>()
                .HasKey(sp => new { sp.SmlouvaId, sp.PoradceId });

            modelBuilder.Entity<SmlouvaPoradce>()
                .HasOne(sp => sp.Smlouva)//SmlouvaPoradce má jednu Smlouvu
                .WithMany(s => s.SmlouvaPoradce)//Smlouva má mnoho SmlouvaPoradce záznamů
                .HasForeignKey(sp => sp.SmlouvaId); //cizí klíč je SmlouvaId

            modelBuilder.Entity<SmlouvaPoradce>()
                .HasOne(sp => sp.Poradce)//SmlouvaPoradce má jednoho Poradce
                .WithMany(p => p.SmlouvaPoradce)//Poradce má mnoho SmlouvaPoradce záznamů
                .HasForeignKey(sp => sp.PoradceId); //cizí klíč je PoradceId

            modelBuilder.Entity<Smlouva>()
                .HasOne(s => s.SpravceSmlouvy)//Smlouva má jednoho Správce smlouvy
                .WithMany(p => p.SpravovaneSmlouvy)// Poradce má více smluv
                .HasForeignKey(s => s.SpravceSmlouvyId)// cizí klíč
                .OnDelete(DeleteBehavior.Restrict);// nemůžeme smazat Poradce,pokud má přiřazené smlouvy 
        }
    }
}
