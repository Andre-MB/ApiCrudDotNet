namespace ApiUdemy.DTOs.Mappings;

public static class CategoriaDTOMappingExtensions
{

    public static CategoriaDTO? ToCategotiaDTO(this CategoriaDTO categoriaDTO)
    {
        if (categoriaDTO == null)
        {
            return null;
        }

        return new CategoriaDTO
        {
            Id = categoriaDTO.Id,
            Nome = categoriaDTO.Nome,
            ImagemUrl = categoriaDTO.ImagemUrl,
        };
    }


}
