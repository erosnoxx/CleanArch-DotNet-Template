using Application.Contracts.Repositories.Common;
using Domain.Entities;

namespace Application.Contracts.Repositories
{
    public interface IRestauranteRepository : IBaseRepository<RestauranteEntity>
    {
        Task<RestauranteEntity?> BuscarRestaurantePorNomeAsync(string nome);
    }
}
