

namespace Dominio.Interfaces;

    public interface IUnitOfWork
    {
         IUsuario Usuarios {get;}
         IRol Roles {get;}
         IHamburguesa Hamburguesas {get;}
         IIngrediente Ingredientes {get;}
         IChef Chefs {get;}
         ICategoria Categorias {get;}
         IHamburguesaIngrediente HamburguesasIngredientes {get;}
        Task<int> SaveAsync();
    }

