
using Application.Models.Dtos.Token;

namespace Application.Contracts.Services
{
    public interface ITokenService
    {
        public TokenOutputDto GenerateToken(string id, string email);
    }
}
