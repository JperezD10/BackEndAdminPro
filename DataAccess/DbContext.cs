using Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DataAccess
{
    public class DbContext<T> : IDbContext<T> where T : class, IEntity
    {
        protected DbSet<T> _items { get; private set; }
        ApiDbContext _context;

        public DbContext(ApiDbContext context)
        {
            _context = context;
            _items = context.Set<T>();
        }

        public void Delete(int id)
        {
            var entity = this.GetById(id);
            if (entity != null)
            {
                _context.Remove(entity);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await this.GetByIdAsync(id);
            if (entity != null)
            {
                _items.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public IList<T> GetAll()
        {
            return _items.ToList();
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await _items.ToListAsync();
        }

        public T GetById(int id)
        {
            return _items.Where(x => x.Id == id).FirstOrDefault();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _items.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public T Save(T entity)
        {
            if (entity.Id == 0)
            {
                _items.Add(entity);
            }
            else
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
            _context.SaveChanges();
            return entity;
        }

        public async Task<T> SaveAsync(T entity)
        {
            if (entity.Id == 0)
            {
                _items.Add(entity);
            }
            else
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}