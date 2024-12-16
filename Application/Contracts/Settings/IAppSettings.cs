using Application.Models.Settings;

namespace Application.Contracts.Settings
{
    public interface IAppSettings
    {
        public ConnectionStringSettings ConnectionStringSettings { get; set; }
        public JwtSettings JwtSettings { get; set; }
        public ConfigurationsSettings ConfigurationsSettings { get; set; }
        public AdminSettings AdminSettings { get; set; }
    }
}
