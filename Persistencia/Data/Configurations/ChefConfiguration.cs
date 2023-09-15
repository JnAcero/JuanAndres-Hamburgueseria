
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class ChefConfiguration : IEntityTypeConfiguration<Chef>
    {
        public void Configure(EntityTypeBuilder<Chef> builder)
        {
            builder.Property(c =>c.Nombre).HasMaxLength(150)
            .IsRequired();
            builder.Property(c =>c.Especialidad).HasMaxLength(100)
            .IsRequired();

        }
    }
}