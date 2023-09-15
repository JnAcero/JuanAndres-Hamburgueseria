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
    public class HamburguesaIngredienteController : BaseApiController
    {
        public HamburguesaIngredienteController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        [HttpPost("Id_Hamburguesa/{id:int}")]
        public async Task<ActionResult> AñadirIngredientesHamburguesa(int id,IngredienteDto[] ingredeintesDtos)
        {
            var hamburguesa = await _unitOfWork.Hamburguesas.GetByIdAsync(id);
            if (hamburguesa == null)
            {
                return NotFound();
            }
            foreach(var ingrediente in ingredeintesDtos)
            {
                var ingredienteExiste = await _unitOfWork.Ingredientes.GetByIdAsync(ingrediente.IngredienteId);
                if(ingredienteExiste is null)
                {
                    return NotFound();
                }
                hamburguesa.HamburguesasIngredientes.Add(new HamburguesaIngrediente(){
                    Hamburguesa = hamburguesa,
                    Ingrediente = ingredienteExiste
                });
            }
            await _unitOfWork.SaveAsync();
            return Ok();
        }

       

    }
}