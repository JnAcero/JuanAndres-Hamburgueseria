

namespace Dominio.Entities
{
    public class Ingrediente: BaseEntity
    {
        public string Nombre { get; set; }
        public string ? Descripcion { get; set; }
        public float Precio { get; set; }
        public float Stock { get; set; }
        public List<HamburguesaIngrediente> HamburguesasIngredientes { get; set; }= new List<HamburguesaIngrediente>();
        
    }
}