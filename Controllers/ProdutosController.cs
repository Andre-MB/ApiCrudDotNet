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
        private readonly IProdutoRepository _repository;

        public ProdutosController(IProdutoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            var produtos = _repository.GetProdutos().ToList();
            if(produtos is null)
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
            var produto = _repository.GetProduto(id);

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

            var novoProduto = _repository.Create(produto);

            return new CreatedAtRouteResult("ObterProduto", new { id = novoProduto.ProdutoId }, novoProduto);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            if(id != produto.ProdutoId)
            {
                return BadRequest(); //400
            }

            bool atualizado = _repository.Update(produto);

            if (atualizado)
            {
                return Ok(produto);
            }
            else
            {
                return StatusCode(500, $"Falha ao atualizar o produdo de id = {id}");
            }

        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {

            bool atualizado = _repository.Delete(id);

            if (atualizado)
            {
                return Ok($"Produto de id={id} foi excluido");
            }
            else
            {
                return StatusCode(500, $"Falha ao excluir o produto de id={id}");
            }
        }

    }
}
