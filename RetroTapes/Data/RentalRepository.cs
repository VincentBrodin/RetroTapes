using Microsoft.EntityFrameworkCore;
using RetroTapes.Data;
using RetroTapes.Models;
using System.Linq.Expressions;

public class RentalRepository(SakilaContext context) : IRepository<Rental>
{
    private IQueryable<Rental> RentalsWithIncludes(bool tracking = false)
    {
        var query = context.Rentals
            .Include(r => r.Inventory).ThenInclude(i => i.Film)
            .Include(r => r.Customer)
            .Include(r => r.Staff);

        return tracking ? query : query.AsNoTracking();
    }

    public IEnumerable<Rental> All()
    {
        return RentalsWithIncludes().ToList();
    }

    public IQueryable<Rental> Query()
    {
        return RentalsWithIncludes();
    }

    public IEnumerable<Rental> Find(Expression<Func<Rental, bool>> predicate)
    {
        return RentalsWithIncludes().Where(predicate).ToList();
    }

    public Rental? Get(int id)
    {
        return RentalsWithIncludes(true).FirstOrDefault(r => r.RentalId == id);
    }

    public void Add(Rental entity) => context.Add(entity);
    public void Update(Rental entity) => context.Update(entity);
    public void Delete(int id)
    {
        var entity = Get(id) ?? throw new NullReferenceException($"No rental with id {id}!");
        context.Remove(entity);
    }
    public void SaveChanges() => context.SaveChanges();

    public async Task AddAsync(Rental entity) => await context.AddAsync(entity);
    public Task UpdateAsync(Rental entity) { context.Update(entity); return Task.CompletedTask; }
    public async Task<Rental?> GetAsync(int id) => await RentalsWithIncludes(true).FirstOrDefaultAsync(r => r.RentalId == id);
    public async Task DeleteAsync(int id)
    {
        var entity = await GetAsync(id) ?? throw new NullReferenceException($"No entity with id {id}!");
        context.Remove(entity);
    }
    public async Task<IEnumerable<Rental>> AllAsync() => await RentalsWithIncludes().ToListAsync();
    public async Task<IEnumerable<Rental>> FindAsync(Expression<Func<Rental, bool>> predicate) => await RentalsWithIncludes().Where(predicate).ToListAsync();
    public async Task SaveChangesAsync() => await context.SaveChangesAsync();
}
