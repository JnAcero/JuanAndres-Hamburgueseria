

using Dominio.Entities;

namespace Dominio.Interfaces
{
    public interface IHamburguesa:IGenericRepository<Hamburguesa>
    {
        Task<List<Hamburguesa>> GetHamburguesasAync();
        Task<List<Hamburguesa>> GetHambVegetarianas();
        Task<List<Hamburguesa>> GetHamburguesaByChef(string nombreChef);
        void AgregarIngredienteAClasica(Ingrediente ingrediente,Hamburguesa hamburguesa);
        Task<List<HamburguesaIngrediente>> GetHamburguesasByIngrediente(int ingredienteId);
        Task<IEnumerable<Hamburguesa>> Menoresde9Dolares();
    }
}