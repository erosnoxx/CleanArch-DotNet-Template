using Data.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Application.Models.Dtos.Common;
using Application.Contracts.Repositories.Common;

namespace Data.Repositories.Common
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly DatabaseContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(
            DatabaseContext dbContext
        )
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> DeleteAsync(T entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int? id)
        {
            return await _dbSet
                 .AsNoTracking()
                 .FirstOrDefaultAsync(p => p.Id.Equals(id));
        }

        public async Task<PagedResult<T>> GetAllPaginatedAsync(int pageSize, int pageNumber)
        {
            var totalRecords = await _dbSet.AsNoTracking().CountAsync();
            var data = await _dbSet
                .AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<T>
            {
                Items = data,
                ItensPageCount = data.Count,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalRecords
            };
        }

        public async Task<PagedResult<TResult>> GetAllPaginatedWithDtoAsync<TResult>(int pageSize, int pageNumber, Func<T, TResult> selector)
        {
            var totalRecords = await _dbSet.AsNoTracking().CountAsync();
            var data = await _dbSet
                .AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var items = data.Select(selector).ToList();

            return new PagedResult<TResult>
            {
                Items = items,
                ItensPageCount = data.Count,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalRecords
            };
        }
    }
}
