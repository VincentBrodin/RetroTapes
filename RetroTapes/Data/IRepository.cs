using System.Linq.Expressions;

namespace RetroTapes.Data;

/// <summary>
/// Should be inherited from by all repositories in the code base.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepository<T>
{
    void Add(T entity);
    void Update(T entity);
    T? Get(int id);
    void Delete(int id);
    IEnumerable<T> All();
    IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
    void SaveChanges();

    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task<T?> GetAsync(int id);
    Task DeleteAsync(int id);
    Task<IEnumerable<T>> AllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task SaveChangesAsync();
}
