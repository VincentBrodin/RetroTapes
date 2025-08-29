using System.Linq.Expressions;
using RetroTapes.Models;

namespace RetroTapes.Data;

/// <summary>
///  Is a generic repository for the DAL
/// </summary>
/// <param name="context"></param>
/// <typeparam name="T"></typeparam>
public class GenericRepository<T>(SakilaContext context) : IRepository<T> where T : class
{
    /// <summary>
    /// Adds an entity to the context
    /// </summary>
    /// <param name="entity">A class that exists inside the SakilaContext.cs</param>
    public void Add(T entity)
    {
        context.Add(entity);
    }

    /// <summary>
    /// Gets all the entities in the context for the given class
    /// </summary>
    /// <returns></returns>
    public IEnumerable<T> All()
    {
        return [.. context.Set<T>()];
    }

    /// <summary>
    /// Deletes the entity with the given id
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="NullReferenceException"></exception>
    public void Delete(int id)
    {
        var entity = Get(id) ?? throw new NullReferenceException("Entity is null");
        context.Remove(entity);
    }

    /// <summary>
    /// Runs the given predicate against the context with the set of type T
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
    {
        return context.Set<T>().AsQueryable().Where(predicate);
    }

    /// <summary>
    /// Grabs the entity with the given id from the set with type T
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public T? Get(int id)
    {
        return context.Set<T>().Find(id);
    }

    /// <summary>
    /// Saves all the changes done
    /// </summary>
    public void SaveChanges()
    {
        context.SaveChanges();
    }

    /// <summary>
    /// Updates changes made to an entity (Good to run before save changes)
    /// </summary>
    /// <param name="entity"></param>
    public void Update(T entity)
    {
        context.Update(entity);
    }
}
