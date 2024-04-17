using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Persistence.Configurations
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder
                .Property(e => e.OperationNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
                

            builder.Property(p => p.Total)
                .HasPrecision(11, 2);

            builder.Property(e => e.SaleDate)
                .HasColumnType("date")
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
