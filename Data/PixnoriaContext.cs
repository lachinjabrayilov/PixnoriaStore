using Microsoft.EntityFrameworkCore;
using PixnoriaStore.Models;

namespace PixnoriaStore.Data
{
    public class PixnoriaContext : DbContext
    {
        public PixnoriaContext(DbContextOptions<PixnoriaContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Siparis> Siparisler { get; set; }
        public DbSet<Urun> Urunler { get; set; }
        public DbSet<Favori> Favoriler { get; set; }
        public DbSet<SiparisDetay> SiparisDetay { get; set; } // SiparisDetay DbSet'i eklendi

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Favori ve Urun ilişkisi
            modelBuilder.Entity<Favori>()
                .HasOne(f => f.Urun)
                .WithMany()
                .HasForeignKey(f => f.UrunId)
                .OnDelete(DeleteBehavior.Cascade);

            // Siparis ve SiparisDetay ilişkisi
            modelBuilder.Entity<Siparis>()
                .HasMany(s => s.SiparisDetaylar)
                .WithOne(sd => sd.Siparis)
                .HasForeignKey(sd => sd.SiparisId)
                .OnDelete(DeleteBehavior.Cascade);

            // SiparisDetay ve Urun ilişkisi
            modelBuilder.Entity<SiparisDetay>()
                .HasOne(sd => sd.Urun)
                .WithMany()
                .HasForeignKey(sd => sd.UrunId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
