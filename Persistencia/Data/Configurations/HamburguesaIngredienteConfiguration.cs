

using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class HamburguesaIngredienteConfiguration : IEntityTypeConfiguration<HamburguesaIngrediente>
    {
        public void Configure(EntityTypeBuilder<HamburguesaIngrediente> builder)
        {
            builder.HasKey(hi =>new{hi.HamburguesaId, hi.IngredienteId});
        }
    }
}