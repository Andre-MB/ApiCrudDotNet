using ApiUdemy.Context;
using System.Linq.Expressions;

namespace ApiUdemy.Repositories;

public class Repository<T>: IRepository<T> where T : class
{
    protected readonly ApiDbContext _context;

    public Repository(ApiDbContext context)
    {
        _context = context;
    }

    public IEnumerable<T> GetAll()
    {
        throw new NotImplementedException();
    }

    public T? Get(Expression<Func<T, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public T Create(T entity)
    {
        throw new NotImplementedException();
    }

    public T Update(T entity)
    {
        throw new NotImplementedException();
    }

    public T Delete(T entity)
    {
        throw new NotImplementedException();
    }

}
