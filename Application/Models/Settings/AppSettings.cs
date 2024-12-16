using Application.Contracts.Settings;
using Microsoft.Extensions.Configuration;

namespace Application.Models.Settings
{
    public class AppSettings : IAppSettings
    {
        public ConnectionStringSettings ConnectionStringSettings { get; set; } = null!;
        public JwtSettings JwtSettings { get; set; } = null!;
        public ConfigurationsSettings ConfigurationsSettings { get; set; } = null!;
        public AdminSettings AdminSettings { get; set; } = null!;

        public static AppSettings FromConfiguration(IConfiguration configuration)
        {
            var appSettings = new AppSettings();

            appSettings.ConnectionStringSettings = configuration.GetSection("ConnectionStrings").Get<ConnectionStringSettings>() ?? throw new ArgumentException("Não pode ser vazio", nameof(ConnectionStringSettings));
            appSettings.JwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>() ?? throw new ArgumentException("Não pode ser vazio", nameof(JwtSettings));
            appSettings.ConfigurationsSettings = configuration.GetSection("ConfigurationsSettings").Get<ConfigurationsSettings>() ?? throw new ArgumentException("Não pode ser vazio", nameof(ConfigurationsSettings));
            appSettings.AdminSettings = configuration.GetSection("AdminSettings").Get<AdminSettings>() ?? throw new ArgumentException("Não pode ser vazio", nameof(AdminSettings));

            return appSettings;
        }
    }
}
