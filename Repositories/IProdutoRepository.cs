using ApiUdemy.Models;

namespace ApiUdemy.Repositories;

public interface IProdutoRepository : IRepository<Produto>
{
    //IQueryable<Produto> GetProdutos();
    //Produto GetProduto(int id);
    //Produto Create(Produto produto);
    //bool Update(Produto produto);
    //bool Delete(int id);

    IEnumerable<Produto> GetProdutosPorCategoria(int id);
}
