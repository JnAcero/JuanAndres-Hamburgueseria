

using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entities
{
    public class HamburguesaIngrediente
    {
        public int HamburguesaId { get; set; }
        [ForeignKey("HamburguesaId")]
        public Hamburguesa Hamburguesa { get; set; }
        public int IngredienteId { get; set; }
        [ForeignKey("IngredienteId")]
        public Ingrediente Ingrediente { get; set; }
        
    }
}