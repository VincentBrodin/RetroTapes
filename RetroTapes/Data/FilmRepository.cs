using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RetroTapes.Models;

namespace RetroTapes.Data;

public class FilmRepository(SakilaContext context) : IRepository<Film>
{
    public void Add(Film entity)
    {
        context.Add(entity);
    }

    public void Update(Film entity)
    {
        context.Update(entity);
    }

    public Film? Get(int id)
    {
        return context.Films.Include(f => f.Language).FirstOrDefault(c => c.FilmId == id);
    }

    public void Delete(int id)
    {
        var entity = Get(id) ?? throw new NullReferenceException($"No film with id {id}!");
        context.Remove(entity);
    }

    public IEnumerable<Film> All()
    {
        return context.Films.Include(f => f.Language);
    }

    public IEnumerable<Film> Find(Expression<Func<Film, bool>> predicate)
    {
        return context.Films.Include(f => f.Language).Where(predicate);
    }

    public void SaveChanges()
    {
        context.SaveChanges();
    }

    public async Task AddAsync(Film entity)
    {
        await context.AddAsync(entity);
    }

    public Task UpdateAsync(Film entity)
    {
        context.Update(entity);
        return Task.CompletedTask;
    }

    public async Task<Film?> GetAsync(int id)
    {
        return await context.Films.Include(f => f.Language).FirstOrDefaultAsync(c => c.FilmId == id);
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetAsync(id) ?? throw new NullReferenceException($"No film with id {id}!");
        context.Remove(entity);
    }

    public async Task<IEnumerable<Film>> AllAsync()
    {
        return await context.Films.Include(f => f.Language).ToListAsync();
    }

    public async Task<IEnumerable<Film>> FindAsync(Expression<Func<Film, bool>> predicate)
    {

        return await context.Films.Include(f => f.Language).Where(predicate).ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}
