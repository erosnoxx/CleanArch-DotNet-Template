using System.Text.Json.Serialization;

namespace Application.Models.Dtos.User.Authenticate
{
    public class UserAuthenticateOutputDto
    {
        [JsonPropertyName("token")]
        public string Id { get; set; } = null!;
    }
}
