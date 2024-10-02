using ApiUdemy.Context;
using ApiUdemy.DTOs;
using ApiUdemy.Models;
using ApiUdemy.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiUdemy.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        // private readonly IRepository<Produto> _repository;
        // só a injeçao dp repositorio especifico e nescessaria pq ela contem a do generico
        // private readonly IProdutoRepository _produtoRepository;

        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public ProdutosController(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }

        //public ProdutosController(IProdutoRepository repository, IProdutoRepository produtoRepository)
        //{
        //    _repository = repository;
        //    _produtoRepository = produtoRepository; 
        //}

        [HttpGet("produtos/{id}")]
        public ActionResult <IEnumerable<ProdutoDTO>> GetProdutosCategoria(int id)
        {
            //  var produtos = _repository.GetProdutosPorCategoria(id);
            var produtos = _uof.ProdutoRepository.GetProdutosPorCategoria(id);

            if(produtos is null)
                return NotFound();

            // var destino = _mapper.Map<Destinio>(origem);
            var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

            return Ok(produtosDto);
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProdutoDTO>> Get()
        {
            // var produtos = _repository.GetAll().ToList();
            var produtos =  _uof.ProdutoRepository.GetAll().ToList();

            if (produtos is null)
            {
                return NotFound("Produtos não encontrados...");
            }

            var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

            return Ok(produtosDto);
        }

        //[HttpGet("primeiro")]
        //[HttpGet("/primeiro")]
        // [HttpGet("{valor:alpha:length(5)}")]
        //public ActionResult<Produto> Get2(string valor)
        //{
        //    var teste = valor;
        //    return _context.Produtos.FirstOrDefault();
        //}
         


        [HttpGet("{id}", Name="ObterProduto")]
        public ActionResult<ProdutoDTO> Get(int id)
        {
            var produto = _uof.ProdutoRepository.Get(p=> p.ProdutoId == id);

            if (produto == null)
            {
                return NotFound("Produto não encontrado");
            }

            var produtoDto = _mapper.Map<ProdutoDTO>(produto);

            return Ok(produtoDto);
        }

        [HttpPost]
        public ActionResult<ProdutoDTO> Post(ProdutoDTO produtoDto)
        {
            if (produtoDto is null) return BadRequest();

            var produto = _mapper.Map<Produto>(produtoDto);

            var novoProduto = _uof.ProdutoRepository.Create(produto);
            _uof.Commit();

            var novoProdutoDto = _mapper.Map<ProdutoDTO>(novoProduto);

            return new CreatedAtRouteResult("ObterProduto", new { id = novoProdutoDto.ProdutoId }, novoProdutoDto);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ProdutoDTO> Put(int id, ProdutoDTO produtoDto)
        {
            if(id != produtoDto.ProdutoId)
                return BadRequest(); //400

            var produto = _mapper.Map<Produto>(produtoDto);

            var produtoAtualizado = _uof.ProdutoRepository.Update(produto);
            _uof.Commit();

            var produtoAtualizadoDto = _mapper.Map<ProdutoDTO>(produtoAtualizado);

           return Ok(produtoAtualizadoDto);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<ProdutoDTO> Delete(int id)
        {

            var produto = _uof.ProdutoRepository.Get(p => p.ProdutoId == id);

            if (produto == null)
            {
                return NotFound("Produto não encontrado");
            }

            var produtoDeletado = _uof.ProdutoRepository.Delete(produto);
            _uof.Commit();

            var produtoDeletadoDto = _mapper.Map<ProdutoDTO>(produtoDeletado);

            return Ok(produtoDeletadoDto);
        }


        [HttpPatch("{id}/UpdatePartial")]
        public ActionResult<ProdutoDTOUpdateResponse> Patch(int id, JsonPatchDocument<ProdutoDTOUpdateRequest> patchProdutoDTO)
        {
            if(patchProdutoDTO is null || id <= 0)
            {
                return BadRequest();
            }

            var produto = _uof.ProdutoRepository.Get(c=>c.ProdutoId == id);

            if (produto == null)
                return NotFound();

            var produtoUpdateRequest = _mapper.Map<ProdutoDTOUpdateRequest>(produto);

            patchProdutoDTO.ApplyTo(produtoUpdateRequest, ModelState);

            if (!ModelState.IsValid || TryValidateModel(produtoUpdateRequest))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(produtoUpdateRequest, produto);

            _uof.ProdutoRepository.Update(produto);
            _uof.Commit();

            return Ok(_mapper.Map<ProdutoDTOUpdateResponse>(produto));
        }

    }
}
