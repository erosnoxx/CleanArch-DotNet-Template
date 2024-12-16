using Application.Contracts.Repositories;
using Application.Contracts.Services;
using Application.Contracts.UseCases.Restaurantes;
using Application.Exceptions;
using Application.Models.Dtos.Restaurante;
using Domain.Entities;

namespace Application.UseCases.Restaurantes
{
    public class RestauranteCreateUseCase(
        IRestauranteRepository restauranteRepository,
        ICriptografiaService criptografiaService
        ) : IRestauranteCreateUseCase
    {
        private readonly IRestauranteRepository _restauranteRepository = restauranteRepository;
        private readonly ICriptografiaService _criptografiaService = criptografiaService;

        public async Task<RestauranteCreateOutputDto> ExecutaAsync(RestauranteCreateInputDto input)
        {
            var restauranteEntity = await _restauranteRepository.BuscarRestaurantePorNomeAsync(input.Name);
            if(restauranteEntity is not null)
            {
                throw new ConflictException("Restaurante já criado com esse nome");
            }

            var cnpjEncriptado = await _criptografiaService.EncriptarAsync(input.Cnpj);

            var entity = new RestauranteEntity(input.Name, cnpjEncriptado);
            await _restauranteRepository.CreateAsync(entity);

            // Envio de email para o usuario

            return new RestauranteCreateOutputDto
            {
                Id = entity.Id,
                Cnpj = entity.Cnpj,
                Name = entity.Nome,
                CreatedAt = entity.CreatedAt
            };
        }
    }
}
