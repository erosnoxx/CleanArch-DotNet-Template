using Application.Contracts.Repositories;
using Data.Context;
using Data.Repositories.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class RestauranteRepository(DatabaseContext dbContext) : BaseRepository<RestauranteEntity>(dbContext), IRestauranteRepository
    {
        public async Task<RestauranteEntity?> BuscarRestaurantePorNomeAsync(string nome)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Nome.Equals(nome));
        }
    }
}
