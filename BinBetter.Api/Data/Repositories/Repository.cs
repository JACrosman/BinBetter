﻿using Microsoft.EntityFrameworkCore;

namespace BinBetter.Api.Data.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected readonly BinBetterContext _context;

        public Repository(BinBetterContext context)
        {
            _context = context;
        }

        public virtual async Task<IEnumerable<T>> ListAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public virtual IQueryable<T> QueryableAsync()
        {
            return _context.Set<T>().AsNoTracking().AsQueryable();
        }

        public virtual async Task<T?> FindByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Set<T>().FindAsync(id, cancellationToken);
        }

        public virtual void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public virtual void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public virtual void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}
