using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository
{
    public class HamburguesaRepository : GenericRepository<Hamburguesa>, IHamburguesa
    {
        public HamburguesaRepository(DbAppContext context) : base(context)
        {
        }

        public async Task<List<Hamburguesa>> GetHamburguesasAync()
        {
            var hamburguesas =await  _context.Hamburguesas
            .Include(x => x.Categoria)
            .Include(x =>x.Chef)
            .Include(x =>x.HamburguesasIngredientes).
                ThenInclude(x =>x.Ingrediente)
            .ToListAsync();

            return hamburguesas;

        }
        public async Task<List<Hamburguesa>> GetHambVegetarianas()
        {
            var veggies = await _context.Hamburguesas
            .Where(h =>h.Categoria.Nombre.ToLower() == "vegetariana" || h.Categoria.Nombre.ToLower() == "vegetariano" )
            .Include(h =>h.Categoria)
            .Include(h=>h.Chef)
            .ToListAsync();
            return veggies;
        }
        public async Task<List<Hamburguesa>> GetHamburguesaByChef(string nombreChef)
        {
            var hamburguesas = await GetHamburguesasAync();
            var result =  hamburguesas.Where(h =>h.Chef.Nombre.ToLower() == nombreChef.ToLower());
            return result.ToList();
        }
        public void AgregarIngredienteAClasica(Ingrediente ingrediente,Hamburguesa hamburguesa)
        {
            hamburguesa.HamburguesasIngredientes.Add(new HamburguesaIngrediente(){
                Hamburguesa = hamburguesa,
                Ingrediente = ingrediente
            });   
        }
         public async Task<List<HamburguesaIngrediente>> GetHamburguesasByIngrediente(int ingredienteId)
        {
            var hamburguesasIngredeintes = 
            await _context.HamburguesasIngredientes.Where(x =>x.IngredienteId == ingredienteId)
            .Include(x =>x.Hamburguesa)
            .Include(x=>x.Ingrediente)
            .ToListAsync();
            return hamburguesasIngredeintes;
        } 
        public async Task<IEnumerable<Hamburguesa>> Menoresde9Dolares()
        {
            var hamburguesas = await GetHamburguesasAync();
            var result = hamburguesas.Where(x =>x.Precio <=9).ToList();
            return result;
        }
    }
}