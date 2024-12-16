using Application.Contracts.Services;
using Application.Contracts.Settings;
using Application.Contracts.UseCases.Restaurantes;
using Application.Models.Settings;
using Application.Services;
using Application.UseCases.Restaurantes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettings = AppSettings.FromConfiguration(configuration);
            services.AddSingleton<IAppSettings>(appSettings);

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IDatetimeUtilsService, DatetimeUtilsService>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IRestauranteCreateUseCase, RestauranteCreateUseCase>();
            return services;
        }
    }
}
