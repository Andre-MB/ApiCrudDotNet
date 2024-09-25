using ApiUdemy.Models;
using AutoMapper;

namespace ApiUdemy.DTOs.Mappings;

public class ProdutoDTOMappingProfile : Profile
{
    public ProdutoDTOMappingProfile() 
    {
            CreateMap<Produto,ProdutoDTO>().ReverseMap();
            CreateMap<Categoria,CategoriaDTO>().ReverseMap();
    }
}
