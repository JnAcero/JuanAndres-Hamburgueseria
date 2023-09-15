
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}