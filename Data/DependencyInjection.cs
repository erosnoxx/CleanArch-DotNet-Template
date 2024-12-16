using Application.Contracts.Repositories;
using Application.Contracts.Repositories.Common;
using Application.Contracts.Settings;
using Data.Context;
using Data.Repositories;
using Data.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            var appSettingsService = services.BuildServiceProvider().GetService<IAppSettings>();

            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseNpgsql(appSettingsService?.ConnectionStringSettings.Default, options =>
                    options.MigrationsHistoryTable(
                        appSettingsService.ConfigurationsSettings.DatabaseMigrationHistoryTable,
                        appSettingsService.ConfigurationsSettings.DatabaseSchema)
                );
            });


            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IRestauranteRepository, RestauranteRepository>();

            return services;
        }
    }
}
