using ApiUdemy.Context;
using ApiUdemy.Models;
using ApiUdemy.Repositories;
using Microsoft.AspNetCore.Http;
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

        public ProdutosController(IUnitOfWork uof)
        {
            _uof = uof;
        }

        //public ProdutosController(IProdutoRepository repository, IProdutoRepository produtoRepository)
        //{
        //    _repository = repository;
        //    _produtoRepository = produtoRepository; 
        //}

        [HttpGet("produtos/{id}")]
        public ActionResult <IEnumerable<Produto>> GetProdutosCategoria(int id)
        {
            //  var produtos = _repository.GetProdutosPorCategoria(id);
            var produtos = _uof.ProdutoRepository.GetProdutosPorCategoria(id);

            if(produtos is null)
                return NotFound();

            return Ok(produtos);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            // var produtos = _repository.GetAll().ToList();
            var produtos =  _uof.ProdutoRepository.GetAll().ToList();

            if (produtos is null)
            {
                return NotFound("Produtos não encontrados...");
            }
            return Ok(produtos);
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
        public ActionResult<Produto> Get(int id)
        {
            var produto = _uof.ProdutoRepository.Get(p=> p.ProdutoId == id);

            if (produto == null)
            {
                return NotFound("Produto não encontrado");
            }
            return Ok(produto);
        }

        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            if (produto is null) return BadRequest();

            var novoProduto = _uof.ProdutoRepository.Create(produto);
            _uof.Commit();

            return new CreatedAtRouteResult("ObterProduto", new { id = novoProduto.ProdutoId }, novoProduto);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            if(id != produto.ProdutoId)
            {
                return BadRequest(); //400
            }

            var produtoAtualizado = _uof.ProdutoRepository.Update(produto);
            _uof.Commit();

           return Ok(produtoAtualizado);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {

            var produto = _uof.ProdutoRepository.Get(p => p.ProdutoId == id);

            if (produto == null)
            {
                return NotFound("Produto não encontrado");
            }

            var produtoDeletado = _uof.ProdutoRepository.Delete(produto);
            _uof.Commit();
            return Ok(produtoDeletado);
        }

    }
}
