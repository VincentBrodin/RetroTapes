using System.Linq.Expressions;
using RetroTapes.Models;

namespace RetroTapes.Data;

public class GenericRepository<T>(SakilaContext context) : IRepository<T> where T : class
{
    public void Add(T entity)
    {
        context.Add(entity);
    }

    public void Delete(T entity)
    {
        context.Remove(entity);
    }

    public IEnumerable<T> All()
    {
        return [.. context.Set<T>()];
    }

    public void Delete(int id)
    {
        var entity = Get(id) ?? throw new NullReferenceException("Entity is null");
        context.Remove(entity);
    }

    public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
    {
        return context.Set<T>().AsQueryable().Where(predicate);
    }

    public T? Get(int id)
    {
        return context.Set<T>().Find(id);
    }

    public void SaveChanges()
    {
        context.SaveChanges();
    }

    public void Update(T entity)
    {
        context.Update(entity);
    }
}
