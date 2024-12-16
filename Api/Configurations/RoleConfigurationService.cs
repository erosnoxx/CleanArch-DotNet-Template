using Application.Models.Role;
using System.Text.Json;

namespace Api.Configurations
{
    public static class RoleConfigurationService
    {
        public static IServiceCollection AddRoleConfigurationService(this IServiceCollection services)
        {
            var jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "Authentication", "roles.json");
            var jsonData = File.ReadAllText(jsonPath);
            var rolesConfig = JsonSerializer.Deserialize<RolesConfig>(jsonData);

            if (rolesConfig is null)
            {
                throw new Exception("Roles configuration not found");
            }

            services.AddSingleton(rolesConfig);

            return services;
        }
    }
}
