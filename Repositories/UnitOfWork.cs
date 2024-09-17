using ApiUdemy.Context;

namespace ApiUdemy.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private IProdutoRepository? _produtoRepo;

    private ICategoriaRepository? _categoriaRepo;

    public ApiDbContext _context;

    public UnitOfWork(ApiDbContext context)
    {
        _context = context;
    }

    public IProdutoRepository ProdutoRepository
    {
        get
        {
            return _produtoRepo = _produtoRepo ?? new ProdutoRepository(_context);
            // if (_produtoRepo == null)
            // {
            //      _produtoRepo =  new ProdutoRepository(_context);
            // }
            // return _produtoRepo
        }
    }

    public ICategoriaRepository CategoriaRepository
    {
        get
        {
            return _categoriaRepo = _categoriaRepo ?? new CategoriaRepository(_context);
        }
    }

    public void Commit()
    {
        throw new NotImplementedException();
    }

    // Libera recursos associados ao banco de dados
    public void Dispose()
    {
        _context.Dispose();
    }
}
