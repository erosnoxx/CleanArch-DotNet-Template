using System.Text.Json.Serialization;

namespace Application.Models.Role
{
    public class RolesConfig
    {
        [JsonPropertyName("roles")]
        public List<Role> Roles { get; set; } = [];
    }
}
