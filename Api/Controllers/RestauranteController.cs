using Application.Contracts.UseCases.Restaurantes;
using Application.Models.Dtos.Restaurante;
using Application.UseCases.Restaurantes;
using Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/restaurantes")]
    public class RestauranteController(
        IRestauranteCreateUseCase restauranteCreateUseCase
        ) : ControllerBase
    {
        private readonly IRestauranteCreateUseCase _restauranteCreateUseCase = restauranteCreateUseCase;

        [HttpPost]
        public async Task<ActionResult<RestauranteCreateOutputDto>> CriarRestaurante(RestauranteCreateInputDto input)
        {
            return Ok(await _restauranteCreateUseCase.ExecutaAsync(input));
        }
    }
}
