﻿using ApiUdemy.Context;
using ApiUdemy.Models;
using ApiUdemy.Pagination;

namespace ApiUdemy.Repositories;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    

    public ProdutoRepository(ApiDbContext context) : base(context)
    {

    }

    //public IEnumerable<Produto> GetProdutos(ProdutoParameters produtoParameters)
    //{
    //    return GetAll()
    //        .OrderBy(p => p.Nome)
    //        .Skip((produtoParameters.PageNumber - 1) * produtoParameters.PageSize)
    //        .Take(produtoParameters.PageSize).ToList();
    //}

    public PagedList<Produto> GetProdutos(ProdutoParameters produtoParameters)
    {
        var produtos = GetAll().OrderBy(p=>p.ProdutoId).AsQueryable();
        var produtosOrdenados = PagedList<Produto>.ToPagedList(produtos, produtoParameters.PageNumber, produtoParameters.PageSize);
        return produtosOrdenados;
    }

    public IEnumerable<Produto> GetProdutosPorCategoria(int id)
    {
        return GetAll().Where(c=> c.CategoriaId == id);
    }

    //public IQueryable<Produto> GetProdutos()
    //{
    //    return _context.Produtos;
    //}

    //public Produto GetProduto(int id)
    //{
    //     var produto = _context.Produtos.FirstOrDefault(p=>p.ProdutoId == id);

    //    if (produto is null)
    //        throw new InvalidOperationException("Produto é null");
        
    //    return produto;
    //}

    //public Produto Create(Produto produto)
    //{
    //    if (produto is null)
    //        throw new InvalidOperationException("Produto é null");

    //    _context.Produtos.Add(produto);
    //    _context.SaveChanges();

    //    return produto;
    //}

    //public bool Update(Produto produto)
    //{
    //    if (produto is null)
    //        throw new InvalidOperationException("Produto é null");

    //    if(_context.Produtos.Any(p=>p.ProdutoId==produto.ProdutoId))
    //    {
    //        _context.Produtos.Update(produto);
    //        _context.SaveChanges();

    //        return true;
    //    }

    //    return false;
    //}

    //public bool Delete(int id)
    //{
    //    var produto = _context.Produtos.Find(id);

    //    if (produto is not null)
    //    {
    //        _context.Produtos.Remove(produto);
    //        _context.SaveChanges();
    //        return true;
    //    }
    //    return false;
    //}
}
