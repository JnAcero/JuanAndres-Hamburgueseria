
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class HamburguesaConfiguration : IEntityTypeConfiguration<Hamburguesa>
    {
        public void Configure(EntityTypeBuilder<Hamburguesa> builder)
        {
            builder.Property(h =>h.Nombre).HasMaxLength(150)
            .IsRequired();
            builder.Property(h =>h.Precio).HasPrecision(5,2)
            .IsRequired();
        }
    }
}