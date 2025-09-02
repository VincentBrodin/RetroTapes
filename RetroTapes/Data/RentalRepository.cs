using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RetroTapes.Models;
using RetroTapes.Data;
using System.Linq.Expressions;

namespace RetroTapes.Data
{
    public class RentalRepository : IRepository<Rental>
    {
        private readonly SakilaContext _context;
        private readonly DbSet<Rental> _set;

        public RentalRepository(SakilaContext context)
        {
            _context = context;
            _set = _context.Set<Rental>();
        }

        // Return all rentals with required navigation loaded for UI lists
        public IEnumerable<Rental> All()
        {
            return _set
                .Include(r => r.Inventory).ThenInclude(i => i.Film)
                .Include(r => r.Customer)
                .Include(r => r.Staff)
                .AsNoTracking()
                .ToList();
        }

        // Apply predicate over the materialized list (IEnumerable<T> pattern in your repo)
        public IEnumerable<Rental> Find(Func<Rental, bool> predicate)
        {
            return All().Where(predicate);
        }

        public IEnumerable<Rental> Find(Expression<Func<Rental, bool>> predicate)
        {
            return _set
                .Include(r => r.Inventory).ThenInclude(i => i.Film)
                .Include(r => r.Customer)
                .Include(r => r.Staff)
                .AsNoTracking()
                .Where(predicate)
                .ToList();
        }

        // Get single rental with navigations
        public Rental? Get(int id)
        {
            return _set
                .Include(r => r.Inventory).ThenInclude(i => i.Film)
                .Include(r => r.Customer)
                .Include(r => r.Staff)
                .FirstOrDefault(r => r.RentalId == id);
        }

        public void Add(Rental entity)
        {
            _set.Add(entity);
        }

        public void Update(Rental entity)
        {
            _set.Update(entity);
        }

        public void Delete(int id)
        {
            var entity = _set.Find(id);
            if (entity != null)
            {
                _set.Remove(entity);
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}

