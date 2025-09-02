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

    public IEnumerable<Customer> All()
    {
        return context.Customers.Include(c => c.Address).Include(c => c.Store);
    }

    public void Delete(int id)
    {
        var entity = Get(id) ?? throw new NullReferenceException($"No customer with id {id}!");
        context.RemoveRange(context.Rentals.Where(r => r.CustomerId == entity.CustomerId));
        context.RemoveRange(context.Payments.Where(p => p.CustomerId == entity.CustomerId));
        context.Remove(entity);
    }

    public IEnumerable<Customer> Find(Expression<Func<Customer, bool>> predicate)
    {
        return context.Customers.Include(c => c.Address).Include(c => c.Store).Where(predicate);
    }

    public Customer? Get(int id)
    {
        return context.Customers.Include(c => c.Address).Include(c => c.Store).ThenInclude(s => s.Address).FirstOrDefault(c => c.CustomerId == id);
    }

    public void SaveChanges()
    {
        context.SaveChanges();
    }

    public void Update(Customer entity)
    {
        context.Update(entity);
    }
}
