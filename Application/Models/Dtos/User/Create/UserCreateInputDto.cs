﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Models.Dtos.User.Create
{
    public class UserCreateInputDto
    {
        [Required]
        [EmailAddress]
        [JsonPropertyName("email")]
        public string Email { get; set; } = null!;

        [Required]
        [JsonPropertyName("password")]
        public string Password { get; set; } = null!;
    }
}
