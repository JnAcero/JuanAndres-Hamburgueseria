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
    public class ChefController : BaseApiController
    {
        public ChefController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpPost("varios")]
        public async Task<ActionResult> PostChef(ChefCreationDTO[] chefDto)
        {
            var chefs = _mapper.Map<Chef[]>(chefDto);
            _unitOfWork.Chefs.AddRange(chefs);
            await _unitOfWork.SaveAsync();
            return Ok(_mapper.Map<ChefCreationDTO[]>(chefs));
        }
        [HttpGet]
        public async Task<ActionResult> GetChefs()
        {
            var chefs = await _unitOfWork.Chefs.GetAllAsync();
            if (chefs is null)
            {
                return NotFound();
            }
            var result = chefs.Select(x => new
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Especialidad = x.Especialidad
            }).ToList();
            return Ok(result);
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutChef(int id, ChefCreationDTO chefDto)
        {
            var chef = _mapper.Map<Chef>(chefDto);
            chef.Id = id;
            _unitOfWork.Chefs.Update(chef);
            await _unitOfWork.SaveAsync();
            return Ok(chef);
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteChef(int id)
        {
            var chef = await _unitOfWork.Chefs.GetByIdAsync(id);
            if (chef is null)
            {
                return NotFound();
            }
            _unitOfWork.Chefs.Remove(chef);
            await _unitOfWork.SaveAsync();
            return Ok();
        }
        [HttpGet("especialidad_Carnes")]
        public async Task<ActionResult> GetChefsEspecialidadCarnes()
        {
            var chefsCarnes = await _unitOfWork.Chefs.GetChefsEspecialidadCarnes();
            if (chefsCarnes is null)
            {
                return NotFound();
            }
            return Ok(chefsCarnes.Select(c => new
            {
                c.Id,
                c.Nombre,
                c.Especialidad
            }));
        }

    }

}