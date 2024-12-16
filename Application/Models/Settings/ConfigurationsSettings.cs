namespace Application.Models.Settings
{
    public class ConfigurationsSettings
    {
        public string DatabaseSchema { get; set; } = string.Empty;
        public string DatabaseMigrationHistoryTable { get; set; } = string.Empty;
    }
}
