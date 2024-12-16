
using Application.Models.Dtos.Common;
using Domain.Entities;

namespace Application.Contracts.Repositories.Common
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T?> GetByIdAsync(int? id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<PagedResult<T>> GetAllPaginatedAsync(int pageSize, int pageNumber);
        Task<PagedResult<TResult>> GetAllPaginatedWithDtoAsync<TResult>(int pageSize, int pageNumber, Func<T, TResult> selector);
        Task<T> DeleteAsync(T entity);

    }
}
