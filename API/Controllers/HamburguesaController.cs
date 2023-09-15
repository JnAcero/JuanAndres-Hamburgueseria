using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    public class HamburguesaController : BaseApiController
    {
        public HamburguesaController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        [HttpPost]
        public async Task<ActionResult> PostHamburguesas(HamburguesaCreationDTO[] hamburguesasDto)
        {
            var hamburguesas = _mapper.Map<Hamburguesa[]>(hamburguesasDto);
            _unitOfWork.Hamburguesas.AddRange(hamburguesas);
            await _unitOfWork.SaveAsync();
            return Ok(_mapper.Map<HamburguesaCreationDTO[]>(hamburguesas));
        }
        [HttpGet]
        public async Task<ActionResult> GetHamburguesas()
        {
            var hamburguesas = await _unitOfWork.Hamburguesas.GetHamburguesasAync();
            var result = hamburguesas.Select(h => new
            {
                h.Id,
                h.Nombre,
                Categoria = h.Categoria.Nombre,
                h.CategoriaId,
                Chef = h.Chef.Nombre,
                h.ChefId,
                h.Precio,
                Ingredeintes = h.HamburguesasIngredientes.Select(x => new { x.IngredienteId, Nombre = x.Ingrediente.Nombre }).ToList()
            });
            return Ok(result);
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutHamburguesa(int id, HamburguesaCreationDTO hamburguesaDto)
        {
            var hamburguesa = _mapper.Map<Hamburguesa>(hamburguesaDto);
            hamburguesa.Id = id;
            _unitOfWork.Hamburguesas.Update(hamburguesa);
            await _unitOfWork.SaveAsync();
            return Ok(hamburguesa);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var hamburguesa = await _unitOfWork.Hamburguesas.GetByIdAsync(id);
            if (hamburguesa is null)
            {
                return NotFound();
            }
            _unitOfWork.Hamburguesas.Remove(hamburguesa);
            await _unitOfWork.SaveAsync();
            return Ok();
        }
        [HttpGet("V1.1/vegetarianas")]
        [MapToApiVersion("1.1")]
        public async Task<ActionResult> GetVegetarianas()
        {
            var vegetarianas = await _unitOfWork.Hamburguesas.GetHambVegetarianas();
            if (vegetarianas is null)
            {
                return NotFound();
            }
            var result = vegetarianas.Select(x => new
            {
                x.Id,
                x.Nombre,
                Categoria = x.Categoria.Nombre,
                x.CategoriaId,
                Chef = x.Chef.Nombre,
                x.Precio
            });
            return Ok(result);
        }
        [HttpGet("getByChef/name/{chefName}")]
        public async Task<ActionResult> GetHamburguesasByChef(string chefName)
        {
            var hamburguersByChef = await _unitOfWork.Hamburguesas.GetHamburguesaByChef(chefName);
            if (hamburguersByChef is null)
            {
                return NotFound();
            }

            return Ok(hamburguersByChef.Select(h => new
            {
                h.Id,
                h.Nombre,
                Categoria = h.Categoria.Nombre,
                Chef = h.Chef.Nombre,
                h.ChefId,
                h.Precio,
                Ingredeintes = h.HamburguesasIngredientes.Select(x => new { x.IngredienteId, Nombre = x.Ingrediente.Nombre }).ToList()
            }));
        }
        [HttpGet("getConPanIntegral")]
        public async Task<ActionResult> GetConPanIntegral()
        {
            var panIntegral = await _unitOfWork.Ingredientes.FindFirstAsync(i => i.Nombre.ToLower() == "pan integral");
            if (panIntegral is null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "No existe el ingredeinte",
                    result = ""
                });
            }
            var hamburguesasConPanIntegral = await _unitOfWork.Hamburguesas.GetHamburguesasByIngrediente(panIntegral.Id);
            return Ok(panIntegral);
            /* return Ok(hamburguesasConPanIntegral.Select(x =>new{
                Nombre = x.Hamburguesa.Nombre,
                Precio = x.Hamburguesa.Precio,
                CategoriaId = x.Hamburguesa.CategoriaId,
                ChefId = x.Hamburguesa.ChefId
            })); */
      
        }
        [HttpGet("precioMenora9Dolares")]
        public async Task<ActionResult> GetprecioMenora9Dolares()
        {
            var hamburguesas = await _unitOfWork.Hamburguesas.Menoresde9Dolares();
            if (hamburguesas is null)
            {
                return NotFound();
            }
            return Ok(hamburguesas.Select(h => new
            {
                h.Id,
                h.Nombre,
                Categoria = h.Categoria.Nombre,
                Chef = h.Chef.Nombre,
                h.ChefId,
                h.Precio,
                Ingredeintes = h.HamburguesasIngredientes.Select(x => new { x.IngredienteId, Nombre = x.Ingrediente.Nombre }).ToList()
            }));
        }
        [HttpGet("enOrdenAscendente")]
        public async Task<ActionResult> GetOrdenAscendente()
        {
            var hamburguesas = await _unitOfWork.Hamburguesas.GetAllAsync();
            var result = hamburguesas.OrderBy(x =>x.Precio);
            return Ok(result);
        }

    }
}