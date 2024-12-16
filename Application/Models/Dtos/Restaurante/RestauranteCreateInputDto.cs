using System.ComponentModel.DataAnnotations;

namespace Application.Models.Dtos.Restaurante
{
    public class RestauranteCreateInputDto
    {
        [Required]
        [MinLength(5)]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MinLength(14)]
        public string Cnpj { get; set; } = string.Empty;
    }
}
