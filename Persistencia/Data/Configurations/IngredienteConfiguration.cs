

using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class IngredienteConfiguration : IEntityTypeConfiguration<Ingrediente>
    {
        public void Configure(EntityTypeBuilder<Ingrediente> builder)
        {
            builder.Property(i =>i.Nombre).HasMaxLength(150)
            .IsRequired();
            builder.Property(i =>i.Descripcion).HasMaxLength(500);
            builder.Property(i =>i.Precio).HasPrecision(6,2)
            .IsRequired();
            builder.Property(i =>i.Stock).HasPrecision(10,2)
            .IsRequired();

        }
    }
}