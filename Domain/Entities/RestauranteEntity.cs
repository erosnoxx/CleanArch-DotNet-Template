namespace Domain.Entities
{
    public class RestauranteEntity : BaseEntity
    {
        public string Nome { get; set; } = null!;

        public string Cnpj { get; set; } = null!;

        public RestauranteEntity(string nome, string cnpj)
        {
            if(string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentNullException(nameof(nome), $"Nome não pode ser vazio");
            }

            if (nome.Length < 5)
            {
                throw new ArgumentException("Nome deve conter pelo menos 5 caracteres", nameof(nome));
            }

            if (nome.Length > 255)
            {
                throw new ArgumentException("Nome deve conter até 255 caracteres", nameof(nome));
            }

            if (string.IsNullOrWhiteSpace(cnpj))
            {
                throw new ArgumentNullException(nameof(cnpj), $"Cnpj não pode ser vazio");
            }

            if (!cnpj.Length.Equals(14) && !cnpj.Length.Equals(18))
            {
                throw new ArgumentNullException(nameof(cnpj), $"Cnpj não pode ser vazio");
            }

            Nome = nome;
            Cnpj = cnpj.Replace(".", "").Replace("-", "");
        }
    }
}
