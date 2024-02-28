using Microsoft.EntityFrameworkCore;
using MusicStore.Domain;

namespace MusicStore.Persistence
{
    //Permite representar la base de datos
    public class MusicStoreDbContext : DbContext
    {
        public MusicStoreDbContext(DbContextOptions<MusicStoreDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //FLUENT API
            //Genre
            modelBuilder.Entity<Genre>()
                .Property(e => e.Name)
                .HasMaxLength(50);
            modelBuilder.Entity<Genre>()
                .HasQueryFilter(p => p.Status == true);


            //Concert
            modelBuilder.Entity<Concert>()
                .Property(p => p.Title)
                .HasMaxLength(100);
            modelBuilder.Entity<Concert>()
                .Property(p => p.Description)
                .HasMaxLength(500);
            modelBuilder.Entity<Concert>()
                .Property(p => p.Place)
                .HasMaxLength(80);
            modelBuilder.Entity<Concert>()
                .Property(p => p.UnitPrice)
                .HasPrecision(11, 2);
            modelBuilder.Entity<Concert>()
                .Property(p => p.DateEvent) //datetime2 se usa por default
                .HasColumnType("datetime");
            modelBuilder.Entity<Concert>()
                .Property(p => p.ImageUrl)
                .IsUnicode(false)
                .HasMaxLength(1000);


        }
    }
}
