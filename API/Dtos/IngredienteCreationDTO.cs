using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class IngredienteCreationDTO
    {
        public string Nombre { get; set; }
        public string ? Descripcion { get; set; }
        public float Precio { get; set; }
        public float Stock { get; set; }
    }
}