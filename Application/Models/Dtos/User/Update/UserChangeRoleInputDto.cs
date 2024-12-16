using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Models.Dtos.User.Update
{
    public class UserChangeRoleInputDto
    {
        [Required]
        [JsonPropertyName("role")]
        public string Role { get; set; } = null!;
    }
}
