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
    public class CategoriaController : BaseApiController
    {
        public CategoriaController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        [HttpPost("varios")]
        public async Task<ActionResult> PostCategorias(CategoriaCreationDTO[] categoriasDto)
        {
            var categorias = _mapper.Map<Categoria[]>(categoriasDto);
            _unitOfWork.Categorias.AddRange(categorias);
            await _unitOfWork.SaveAsync();
            return Ok(_mapper.Map<CategoriaCreationDTO[]>(categorias));
        }
        [HttpGet]
        public async Task<ActionResult> GetCategorias()
        {
            var categorias = await _unitOfWork.Categorias.GetAllAsync();
            var result = categorias.Select(c =>new {
                 c.Id,
                NombreCategoria = c.Nombre,
                c.Descripcion
            });
            return Ok(result);
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutChef(int id, CategoriaCreationDTO categoriaDto)
        {
            var categoria = _mapper.Map<Categoria>(categoriaDto);
            categoria.Id = id;
            _unitOfWork.Categorias.Update(categoria);
            await _unitOfWork.SaveAsync();
            return Ok(categoria);
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteChef(int id)
        {
            var categoria = await _unitOfWork.Categorias.GetByIdAsync(id);
            if (categoria is null)
            {
                return NotFound();
            }
            _unitOfWork.Categorias.Remove(categoria);
            await _unitOfWork.SaveAsync();
            return Ok();
        }

    }
}