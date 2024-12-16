namespace Application.Contracts.Services
{
    public interface ICriptografiaService
    {
        Task<string> EncriptarAsync(string valor); 
        Task<string> DescriptarAsync(string valor); 
    }
}
