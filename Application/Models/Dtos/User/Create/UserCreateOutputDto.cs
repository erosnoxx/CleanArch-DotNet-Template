using System.Text.Json.Serialization;

namespace Application.Models.Dtos.User.Create
{
    public class UserCreateOutputDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;

        [JsonPropertyName("email")]
        public string Email { get; set; } = null!;

        [JsonPropertyName("Role")]
        public string Role { get; set; } = string.Empty;
    }
}
