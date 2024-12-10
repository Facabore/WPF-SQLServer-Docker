using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_SQLSERVER.Abstractions.Interfaces;
using WPF_SQLSERVER.Infraestructure;

namespace WPF_SQLSERVER.Abstractions
{
    public class Repository<T>(AppDbContext context) : IRepository<T> where T : Entity
    {
        private readonly AppDbContext _context = context;
        private readonly DbSet<T> _dbSet = context.Set<T>();
        public async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

        }

        public async Task Delete(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Update(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
