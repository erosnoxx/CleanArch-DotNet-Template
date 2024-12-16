using Application.Enums;
using Application.Models.Dtos.Token;
using Application.Models.Dtos.User.Authenticate;
using Application.Models.Dtos.User.Create;
using Application.Models.Dtos.User.Get;
using Application.Models.Dtos.User.Update;
using Domain.Entities;

namespace Application.Contracts.Services
{
    public interface IUserService
    {
        Task<TokenOutputDto> Authenticate(UserAuthenticateInputDto input);
        Task<UserCreateOutputDto> CreateUserAsync(UserCreateInputDto user);
        Task<UserGetOutputDto> UpdateUserAsync(UserEntity user);
        Task<UserGetOutputDto> ChangeUserRole(string id, UserChangeRoleInputDto input);
        Task DisableUser(string userId, string callerEmail);
        Task EnableUser(string userId, string callerEmail);
        Task<UserGetOutputDto?> GetUserByIdAsync(string userId);
        Task<UserGetOutputDto?> GetUserByIdAndRoleAsync(string userId, ERole role);
        Task<UserGetOutputDto?> GetByEmail(string username);
        Task<IEnumerable<UserGetOutputDto>> GetAllUsersAsync();
    }
}
