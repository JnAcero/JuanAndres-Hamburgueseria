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
    public class IngredienteController : BaseApiController
    {
        public IngredienteController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        [HttpPost("varios")]
        public async Task<ActionResult> PostIngredientes(IngredienteCreationDTO[] ingredientesDto)
        {
            var ingredientes = _mapper.Map<Ingrediente[]>(ingredientesDto);
            _unitOfWork.Ingredientes.AddRange(ingredientes);
            await _unitOfWork.SaveAsync();
            return Ok(_mapper.Map<IngredienteCreationDTO[]>(ingredientes));
        }
        [HttpGet]
        public async Task<ActionResult> GetIngredientes()
        {
            var ingredientes = await _unitOfWork.Ingredientes.GetAllAsync();
            var result = ingredientes.Select(i => new
            {
                i.Id,
                Ingrediente = i.Nombre,
                i.Descripcion,
                i.Precio,
                i.Stock
            });
            return Ok(result);
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutIngrediente(int id, IngredienteCreationDTO ingredienteDto)
        {
            var ingrediente = _mapper.Map<Ingrediente>(ingredienteDto);
            ingrediente.Id = id;
            _unitOfWork.Ingredientes.Update(ingrediente);
            await _unitOfWork.SaveAsync();
            return Ok(ingrediente);
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteIngrediente(int id)
        {
            var ingrediente = await _unitOfWork.Ingredientes.GetByIdAsync(id);
            if (ingrediente is null)
            {
                return NotFound();
            }
            _unitOfWork.Ingredientes.Remove(ingrediente);
            await _unitOfWork.SaveAsync();
            return Ok();
        }

        [HttpGet("V.1.1/stock_menor_400")]
        [MapToApiVersion("1.1")]
        public async Task<ActionResult> GetIngredientesStock()
        {
            var ingredientes = await _unitOfWork.Ingredientes.GetStockMenorA400();
            var result = ingredientes.Select(i => new
            {
                i.Id,
                i.Nombre,
                i.Descripcion,
                i.Precio,
                i.Stock
            });
            return Ok(result);
        }
        [HttpPost("agregarIngredieneteaClasica/{nombreIngrediente}")]
        public async Task<ActionResult> AgregarIngredienteAClasica(string nombreIngrediente)
        {
            var ingrediente = await _unitOfWork.Ingredientes.FindFirstAsync(i => i.Nombre.ToLower() == nombreIngrediente.ToLower());
            if (ingrediente is null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Ingrediente no existe",
                    result = ""
                });
            }
            int IdHamburguesaClasica = 3;
            var hamburguesasClasicas = _unitOfWork.Hamburguesas.Find(x => x.CategoriaId == IdHamburguesaClasica);
            if (hamburguesasClasicas is null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "No existen hamburguesas clasicas",
                    result = ""
                });
            }
            foreach (var hamburguesa in hamburguesasClasicas)
            {
                hamburguesa.HamburguesasIngredientes.Add(new HamburguesaIngrediente()
                {
                    Hamburguesa = hamburguesa,
                    Ingrediente = ingrediente
                });
            }
            await _unitOfWork.SaveAsync();
            return Ok();
        }
        [HttpGet("ingredeinteMasCaro")]
        public async Task<ActionResult> GetMasCaro()
        {
            var ingrediente = await _unitOfWork.Ingredientes.GetMasCaro();
            return Ok(ingrediente);
        }
        [HttpGet("ingredeEntre2Y5Dolares")]
        public async Task<ActionResult> GetingredeEntre2Y5Dolares()
        {
            var ingredeinte = _unitOfWork.Ingredientes.Find(i => i.Precio > 2 && i.Precio < 5);
            return Ok(ingredeinte);
        }
        [HttpPost("cambioDescripcion_Pan")]
        public async Task<ActionResult> cambioDescripcion(Descripcion descripcion)
        {
            var pan = await _unitOfWork.Ingredientes.FindFirstAsync(x => x.Nombre.ToLower() == "pan");
            if (pan is null)
            {
                return NotFound();
            }
            pan.Descripcion = descripcion.descripcion;
            await _unitOfWork.SaveAsync();
            return Ok();


        }
    }
}