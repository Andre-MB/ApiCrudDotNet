using ApiUdemy.Context;
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

    public PagedList<Produto> GetProdutos(ProdutoParameters produtosParameters)
    {
        //IQueryable<T> é apropriado quando você deseja realizar consultas de forma
        //eficiente em uma fonte de dados que pode ser consultada diretamente, como
        //um banco de dados. Ele suporta a consulta diferida e permite que as
        //consultas sejam traduzidas em consultas SQL eficientes quando você está
        //trabalhando com um provedor de banco de dados, como o Entity Framework.
        //------------------------------------------------------------------------
        //IEnumerable<T> é uma interface mais geral que representa uma coleção de
        //objetos em memória. Ela não oferece suporte a consultas diferidas ou tradução
        //de consultas SQL. Isso significa que, ao usar IEnumerable, você primeiro traz
        //todos os dados para a memória e, em seguida, aplica consultas, o que pode ser
        //menos eficiente para grandes conjuntos de dados.
        var produtos = GetAll().OrderBy(p => p.ProdutoId).AsQueryable();

        var produtosOrdenados = PagedList<Produto>.ToPagedList(produtos,
                   produtosParameters.PageNumber, produtosParameters.PageSize);

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
