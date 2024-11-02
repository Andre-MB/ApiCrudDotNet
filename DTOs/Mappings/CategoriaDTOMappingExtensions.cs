using ApiUdemy.Models;

namespace ApiUdemy.DTOs.Mappings;

public static class CategoriaDTOMappingExtensions
{

    public static CategoriaDTO? ToCategotiaDTO(this Categoria categoria)
    {
        if (categoria == null)
        {
            return null;
        }

        return new CategoriaDTO
        {
            Id = categoria.Id,
            Nome = categoria.Nome,
            ImagemUrl = categoria.ImagemUrl,
        };
    }

    public static Categoria? ToCategoria(this CategoriaDTO categoriaDTO)
    {
        if (categoriaDTO is null) return null;

        return new Categoria
        {
            Id = categoriaDTO.Id,
            Nome = categoriaDTO.Nome,
            ImagemUrl = categoriaDTO.ImagemUrl
        };
    }

    public static IEnumerable<CategoriaDTO> ToCategoriaDTOList(this IEnumerable<Categoria> categorias)
    {
        if (categorias is null || !categorias.Any())
        {
            return new List<CategoriaDTO>();
        }

        return categorias.Select(categorias => new CategoriaDTO
        {
            Id = categorias.Id,
            Nome = categorias.Nome,
            ImagemUrl = categorias.ImagemUrl
        }).ToList();
    }
}
