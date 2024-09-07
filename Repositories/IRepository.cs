using System.Linq.Expressions;

namespace ApiUdemy.Repositories;

public interface IRepository<T>
{
    // cuidado pra não violar o principio ISP
    IEnumerable<T> GetAll();
    T? Get(Expression<Func<T, bool>> predicate);
    T Create(T entity);
    
}
