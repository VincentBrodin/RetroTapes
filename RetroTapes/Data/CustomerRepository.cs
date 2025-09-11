using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RetroTapes.Models;

namespace RetroTapes.Data;

public class CustomerRepository(SakilaContext context) : IRepository<Customer>
{
    public void Add(Customer entity)
    {
        context.Add(entity);
    }

    public async Task AddAsync(Customer entity)
    {
        await context.AddAsync(entity);
    }

    public IEnumerable<Customer> All()
    {
        return context.Customers.Include(c => c.Address).Include(c => c.Store);
    }

    public async Task<IEnumerable<Customer>> AllAsync()
    {
        return await context.Customers.Include(c => c.Address).Include(c => c.Store).ToListAsync();
    }

    public void Delete(int id)
    {
        var entity = Get(id) ?? throw new NullReferenceException($"No customer with id {id}!");
        context.RemoveRange(context.Rentals.Where(r => r.CustomerId == entity.CustomerId));
        context.RemoveRange(context.Payments.Where(p => p.CustomerId == entity.CustomerId));
        context.Remove(entity);
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetAsync(id) ?? throw new NullReferenceException($"No customer with id {id}!");
        context.Remove(entity);
    }

    public IEnumerable<Customer> Find(Expression<Func<Customer, bool>> predicate)
    {
        return context.Customers.Include(c => c.Address).Include(c => c.Store).Where(predicate);
    }

    public async Task<IEnumerable<Customer>> FindAsync(Expression<Func<Customer, bool>> predicate)
    {
        return await context.Customers.Include(c => c.Address).Include(c => c.Store).Where(predicate).ToListAsync();
    }

    public Customer? Get(int id)
    {
        return context.Customers.Include(c => c.Address).Include(c => c.Store).ThenInclude(s => s.Address).FirstOrDefault(c => c.CustomerId == id);
    }

    public async Task<Customer?> GetAsync(int id)
    {
        return await context.Customers.Include(c => c.Address).Include(c => c.Store).ThenInclude(s => s.Address).FirstOrDefaultAsync(c => c.CustomerId == id);
    }

    public void SaveChanges()
    {
        context.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }

    public void Update(Customer entity)
    {
        context.Update(entity);
    }

    public Task UpdateAsync(Customer entity)
    {
        context.Update(entity);
        return Task.CompletedTask;
    }
}
