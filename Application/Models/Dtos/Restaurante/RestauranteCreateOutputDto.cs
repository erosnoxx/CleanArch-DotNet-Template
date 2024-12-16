namespace Application.Models.Dtos.Restaurante
{
    public class RestauranteCreateOutputDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Cnpj { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
