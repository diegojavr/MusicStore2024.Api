using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Persistence.Configurations
{
    public class ConcertConfiguration : IEntityTypeConfiguration<Concert>
    {
        public void Configure(EntityTypeBuilder<Concert> builder)
        {
            builder
                .Property(p => p.Title)
                .HasMaxLength(100);
            builder
                .Property(p => p.Description)
                .HasMaxLength(500);
            builder
                .Property(p => p.Place)
                .HasMaxLength(80);
            builder
                .Property(p => p.UnitPrice)
                .HasPrecision(11, 2);
            builder
                .Property(p => p.DateEvent) //datetime2 se usa por default
                .HasColumnType("datetime");
            builder
                 .Property(p => p.ImageUrl)
                 .IsUnicode(false)
                 .HasMaxLength(1000);
        }
    }
}
