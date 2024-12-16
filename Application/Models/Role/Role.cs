using System.Text.Json.Serialization;

namespace Application.Models.Role
{
    public class Role
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("permissions")]
        public List<string> Permissions { get; set; } = [];
    }
}
