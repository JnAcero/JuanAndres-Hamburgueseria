using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository
{
    public class IngredienteRepository : GenericRepository<Ingrediente>, IIngrediente
    {
        public IngredienteRepository(DbAppContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Ingrediente>> GetStockMenorA400()
        {
            var result =await  _context.Ingredientes.Where(i =>i.Stock < 400).ToListAsync();
            return result;
        }
        public Task<Ingrediente> GetMasCaro()
        {
            var result =  _context.Ingredientes.OrderByDescending(i =>i.Precio).Take(1).FirstOrDefaultAsync();
            return result;

        }
    }
}