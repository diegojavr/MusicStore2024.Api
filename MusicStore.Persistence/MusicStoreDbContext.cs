using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicStore.Domain;
using MusicStore.Domain.Info;
using System.Reflection;

namespace MusicStore.Persistence
{
    //Permite representar la base de datos
    public class MusicStoreDbContext : IdentityDbContext<MusicStoreUserIdentity>
    {
        //Deja de heredar de DbContext para ahora heredar de IdentityDbContext<MusicStoreUserIdentity>
        //Para poder usar la tabla de usuario
        public MusicStoreDbContext(DbContextOptions<MusicStoreDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //FLUENT API           

            //añade cada clase que implemente esta interfaz para las configuraciones de tablas
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            //ASPNET USERS (nombre tabla: Usuario)
            modelBuilder.Entity<MusicStoreUserIdentity>(e =>
            {
                e.ToTable("Usuario");
            });


            //ASPNET ROLES 
            modelBuilder.Entity<IdentityRole>(e =>
            {
                e.ToTable("Rol");
            });


            //ASPNET USER ROLES MUCHOS A MUCHOS
            modelBuilder.Entity<IdentityRole<string>>(e =>
            {
                e.ToTable("UsuarioRol");
            });

            //Aqui especificamos como EF Core debe manipular los datos en esta nueva clase
            modelBuilder.Entity<ReportInfo>()
                .HasNoKey(); //no es tabla entonces no tiene key (llave)

            modelBuilder.Entity<ReportInfo>()
                .Property(p => p.Total)
                .HasPrecision(11, 2);


            //Concert
            //modelBuilder.Entity<Concert>()
            //    .Property(p => p.Title)
            //    .HasMaxLength(100);
            //modelBuilder.Entity<Concert>()
            //    .Property(p => p.Description)
            //    .HasMaxLength(500);
            //modelBuilder.Entity<Concert>()
            //    .Property(p => p.Place)
            //    .HasMaxLength(80);
            //modelBuilder.Entity<Concert>()
            //    .Property(p => p.UnitPrice)
            //    .HasPrecision(11, 2);
            //modelBuilder.Entity<Concert>()
            //    .Property(p => p.DateEvent) //datetime2 se usa por default
            //    .HasColumnType("datetime");
            //modelBuilder.Entity<Concert>()
            //    .Property(p => p.ImageUrl)
            //    .IsUnicode(false)
            //    .HasMaxLength(1000);


        }
    }
}
