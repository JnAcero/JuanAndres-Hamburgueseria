

using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entities
{
    [NotMapped]
    public class UsuarioRoles
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public int RolId { get; set; }
        public Rol Rol { get; set; }
    }
}