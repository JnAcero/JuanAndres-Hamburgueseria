using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Dominio.Entities;

namespace API.Profiles
{
    public class HamburgueseriaProfiles : Profile
    {
        public HamburgueseriaProfiles()
        {
            CreateMap<Chef,ChefCreationDTO>().ReverseMap();
            CreateMap<Categoria,CategoriaCreationDTO>().ReverseMap();
            CreateMap<Ingrediente,IngredienteCreationDTO>().ReverseMap();
            CreateMap<Hamburguesa,HamburguesaCreationDTO>().ReverseMap();
            CreateMap<Hamburguesa,HamburguesaDTO>()
                .ForMember(x =>x.IngredientesIDs, opt=>opt.MapFrom(src =>src.HamburguesasIngredientes)).ReverseMap();
            CreateMap<HamburguesaIngrediente,IngredienteDto>().ReverseMap();
        }
    }
}