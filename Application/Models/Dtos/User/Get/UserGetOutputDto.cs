using System.Text.Json.Serialization;

namespace Application.Models.Dtos.User.Get
{
    public class UserGetOutputDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;

        [JsonPropertyName("email")]
        public string Email { get; set; } = null!;

        [JsonPropertyName("role")]
        public string Role { get; set; } = null!;

        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("deactivatedAt")]
        public DateTime? DeactivatedAt { get; set; }

        [JsonPropertyName("enabledAt")]
        public DateTime? EnabledAt { get; set; }

        [JsonPropertyName("deactivatedByEmail")]
        public string DeactivatedByEmail { get; set; } = null!;

        [JsonPropertyName("enabledByEmail")]
        public string EnabledByEmail { get; set; } = null!;
    }
}
