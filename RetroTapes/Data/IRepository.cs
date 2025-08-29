using System.Linq.Expressions;

namespace RetroTapes.Data;

public interface IRepository<T>
{
    void Add(T entity);
    void Update(T entity);
    T? Get(int id);
    void Delete(T entity);
    IEnumerable<T> All();
    IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
    void SaveChanges();
}