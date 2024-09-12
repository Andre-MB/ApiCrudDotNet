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
        return _context.Set<T>().ToList();
    }

    public T? Get(Expression<Func<T, bool>> predicate)
    {
        return _context.Set<T>().FirstOrDefault(predicate);
    }

    public T Create(T entity)
    {
        _context.Set<T>().Add(entity);
        _context.SaveChanges();
        return entity;
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
