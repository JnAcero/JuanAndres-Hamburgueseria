
using Dominio.Entities;

namespace Dominio.Interfaces
{
    public interface IIngrediente:IGenericRepository<Ingrediente>
    {
        Task<IEnumerable<Ingrediente>> GetStockMenorA400();
        Task<Ingrediente> GetMasCaro();
    }
}