using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class HamburguesaCreationDTO
    {
         public string Nombre { get; set; }
        public int CategoriaId { get; set; }
         public int ChefId { get; set; }
        public float Precio { get; set; }    
    }
}