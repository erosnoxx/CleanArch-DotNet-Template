using System.Text.Json.Serialization;

namespace Application.Models.Dtos.Token
{
    public class TokenOutputDto
    {
        [JsonPropertyName("token")]
        public string AccessToken { get; set; } = null!;

        [JsonPropertyName("expiresInMinutes")]
        public int ExpiresInMinutes { get; set; }
    }
}
