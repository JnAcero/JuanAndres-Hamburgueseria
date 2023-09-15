using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository
{
    public class ChefRepository : GenericRepository<Chef>, IChef
    {
        public ChefRepository(DbAppContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Chef>> GetChefsEspecialidadCarnes()
        {
            var chefs = await GetAllAsync();
            return chefs.Where(c =>c.Especialidad.ToLower() == "carnes");
        }
    }
}