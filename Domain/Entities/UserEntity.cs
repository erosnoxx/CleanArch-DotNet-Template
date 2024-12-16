using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class UserEntity : IdentityUser
    {
        public string CompanyName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool Enabled { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeactivatedAt { get; set; }
        public DateTime EnabledAt { get; set; }

        [EmailAddress]
        public string DeactivatedByEmail { get; set; } = string.Empty;

        [EmailAddress]
        public string EnabledByEmail { get; set; } = string.Empty;
    }
}
