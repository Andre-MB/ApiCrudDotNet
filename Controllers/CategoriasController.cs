using ApiUdemy.Context;
using ApiUdemy.DTOs;
using ApiUdemy.Models;
using ApiUdemy.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiUdemy.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
       // private readonly IRepository<Categoria> _repository;
       private readonly IUnitOfWork _uof;

        public CategoriasController( IUnitOfWork uof)
        {

            _uof = uof;
        }


        [HttpGet]
        public ActionResult<IEnumerable<CategoriaDTO>> Get()
        {
            // var categorias = _repository.GetAll();
            var categorias = _uof.CategoriaRepository.GetAll();

            if (categorias is null) return NotFound("Não existem categorias...");

            var categoriasDto = new List<CategoriaDTO>();

            foreach (var categoria in categorias)
            {
                var categoriaDto = new CategoriaDTO
                {
                    Id = categoria.Id,
                    Nome = categoria.Nome,
                    ImagemUrl = categoria.ImagemUrl,
                };

                categoriasDto.Add(categoriaDto);
            }

            return Ok(categoriasDto);

        }


        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<CategoriaDTO> Get(int id)
        {
            try
            {
                var categoria = _uof.CategoriaRepository.Get( c => c.Id == id );   

                if (categoria == null)
                {
                    return NotFound("Categoria com id={id} não encontrada...");
                }

                var categoriaDto = new CategoriaDTO()
                {
                    Id = categoria.Id,
                    Nome = categoria.Nome,
                    ImagemUrl = categoria.ImagemUrl,
                };

                return Ok(categoriaDto);
            }
            catch (Exception)
            {
                   
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tratar a sua solicitação");
            }
        }

        [HttpPost]
        public ActionResult<CategoriaDTO> Post(CategoriaDTO categoriaDto)
        {
            if (categoriaDto == null) {  return BadRequest("Dados invalidos"); }

            var categoria = new Categoria()
            {
                Id = categoriaDto.Id,
                Nome = categoriaDto.Nome,
                ImagemUrl = categoriaDto.ImagemUrl,
            };

            var categoriaCriada = _uof.CategoriaRepository.Create(categoria);
            _uof.Commit();

            var novacategoriaDto = new CategoriaDTO()
            {
                Id = categoriaCriada.Id,
                Nome = categoriaCriada.Nome,
                ImagemUrl = categoriaCriada.ImagemUrl,
            };

            return new CreatedAtRouteResult("ObterCategoria",
                new { id = novacategoriaDto.Id }, novacategoriaDto);
        }

        [HttpPut("{id:int}")]
        public ActionResult<CategoriaDTO> Put(int id, CategoriaDTO categoriaDto)
        {
            if (id != categoriaDto.Id)
            {
                return BadRequest("Dados invalidos");
            }

            var categoria = new Categoria()
            {
                Id = categoriaDto.Id,
                Nome = categoriaDto.Nome,
                ImagemUrl = categoriaDto.ImagemUrl,
            };

            var categoriaAtualizada = _uof.CategoriaRepository.Update(categoria);
            _uof.Commit();

            var categoriaAtualizadaDto = new CategoriaDTO()
            {
                Id = categoriaAtualizada.Id,
                Nome = categoriaAtualizada.Nome,
                ImagemUrl = categoriaAtualizada.ImagemUrl,
            };

            return Ok(categoriaAtualizadaDto);
        }


        [HttpDelete("{id:int}")]
        public ActionResult<CategoriaDTO> Delete(int id)
        {
            var categoria = _uof.CategoriaRepository.Get(c=>c.Id ==id);

            if (categoria == null)
            {
                return NotFound("Categoria com id={id} não encontrada...");
            }

            var categoriaExcluida = _uof.CategoriaRepository.Delete(categoria);
            _uof.Commit();

            var categoriaExcluidaDto = new CategoriaDTO()
            {
                Id = categoriaExcluida.Id,
                Nome = categoriaExcluida.Nome,
                ImagemUrl = categoriaExcluida.ImagemUrl,
            };

            // se usar o repository direto ele já tinha o saveChanges não precisaria do commit()
            return Ok(categoriaExcluidaDto);
        }


    }
}
