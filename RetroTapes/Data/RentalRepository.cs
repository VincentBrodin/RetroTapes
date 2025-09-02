using Microsoft.EntityFrameworkCore;
using RetroTapes.Models;
using System.Linq.Expressions;

namespace RetroTapes.Data
{
    public class RentalRepository(SakilaContext context) : IRepository<Rental>
    {

        // Return all rentals with required navigation loaded for UI lists
        public IEnumerable<Rental> All()
        {
            return context.Rentals
                .Include(r => r.Inventory).ThenInclude(i => i.Film)
                .Include(r => r.Customer)
                .Include(r => r.Staff);
        }

        // Apply predicate over the materialized list (IEnumerable<T> pattern in your repo)
        public IEnumerable<Rental> Find(Func<Rental, bool> predicate)
        {
            return All().Where(predicate);
        }

        public IEnumerable<Rental> Find(Expression<Func<Rental, bool>> predicate)
        {
            return context.Rentals
                .Include(r => r.Inventory).ThenInclude(i => i.Film)
                .Include(r => r.Customer)
                .Include(r => r.Staff)
                .AsNoTracking()
                .Where(predicate);
        }

        // Get single rental with navigations
        public Rental? Get(int id)
        {
            return context.Rentals
                .Include(r => r.Inventory).ThenInclude(i => i.Film)
                .Include(r => r.Customer)
                .Include(r => r.Staff)
                .FirstOrDefault(r => r.RentalId == id);
        }

        public void Add(Rental entity)
        {
            context.Add(entity);
        }

        public void Update(Rental entity)
        {
            context.Update(entity);
        }

        public void Delete(int id)
        {
            var entity = Get(id) ?? throw new NullReferenceException($"No rental with id {id}!");
            context.Remove(entity);

        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public async Task AddAsync(Rental entity)
        {
            await context.AddAsync(entity);
        }

        public Task UpdateAsync(Rental entity)
        {
            context.Update(entity);
            return Task.CompletedTask;
        }

        public async Task<Rental?> GetAsync(int id)
        {
            return await context.Rentals
                .Include(r => r.Inventory).ThenInclude(i => i.Film)
                .Include(r => r.Customer)
                .Include(r => r.Staff)
                .FirstOrDefaultAsync(r => r.RentalId == id);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(id) ?? throw new NullReferenceException($"No entity with id {id}!");
            context.Remove(entity);
        }

        public async Task<IEnumerable<Rental>> AllAsync()
        {

            return await context.Rentals
                .Include(r => r.Inventory).ThenInclude(i => i.Film)
                .Include(r => r.Customer)
                .Include(r => r.Staff).ToListAsync();
        }

        public async Task<IEnumerable<Rental>> FindAsync(Expression<Func<Rental, bool>> predicate)
        {

            return await context.Rentals
                .Include(r => r.Inventory).ThenInclude(i => i.Film)
                .Include(r => r.Customer)
                .Include(r => r.Staff)
                .Where(predicate)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
