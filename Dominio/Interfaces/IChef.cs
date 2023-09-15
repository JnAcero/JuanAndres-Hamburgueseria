
using Dominio.Entities;

namespace Dominio.Interfaces
{
    public interface IChef:IGenericRepository<Chef>
    {
        Task<IEnumerable<Chef>> GetChefsEspecialidadCarnes();
        
    }
}