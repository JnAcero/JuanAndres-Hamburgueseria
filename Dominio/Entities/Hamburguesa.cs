
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Dominio.Entities
{
    public class Hamburguesa: BaseEntity
    {
        public string Nombre { get; set; }
        public int CategoriaId { get; set; }
        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; }
        public float Precio { get; set; }
        public int ChefId { get; set; }
        [ForeignKey("ChefId")]
        public Chef Chef { get; set; }
         public List<HamburguesaIngrediente> HamburguesasIngredientes { get; set; }= new List<HamburguesaIngrediente>();
        
    }
}