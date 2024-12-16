using Application.Models.Dtos.Restaurante;

namespace Application.Contracts.UseCases.Restaurantes
{
    public interface IRestauranteCreateUseCase
    {
        Task<RestauranteCreateOutputDto> ExecutaAsync(RestauranteCreateInputDto input);
    }
}
