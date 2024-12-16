using Application.Contracts.Services;
using Application.Enums;
using Application.Exceptions;
using Application.Models.Dtos.Token;
using Application.Models.Dtos.User.Authenticate;
using Application.Models.Dtos.User.Create;
using Application.Models.Dtos.User.Get;
using Application.Models.Dtos.User.Update;
using Application.Models.Role;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly RolesConfig _rolesConfig;
        private readonly ITokenService _tokenService;
        private readonly IDatetimeUtilsService _datetimeUtilsService;

        public UserService(
            UserManager<UserEntity> userManager,
            RolesConfig rolesConfig,
            ITokenService tokenService,
            IDatetimeUtilsService datetimeUtilsService
        )
        {
            _userManager = userManager;
            _rolesConfig = rolesConfig;
            _tokenService = tokenService;
            _datetimeUtilsService = datetimeUtilsService;
        }

        public async Task<TokenOutputDto> Authenticate(UserAuthenticateInputDto input)
        {
            var user = await _userManager.FindByEmailAsync(input.Email);

            if (user is null)
                throw new UnauthorizedAccessException("Usuário ou senha inválidos");

            if (!user.Enabled)
                throw new UnauthorizedAccessException("Seu usuário está inativo. Contate um administrador!");

            var result = await _userManager.CheckPasswordAsync(user, input.Password);

            if (!result)
                throw new UnauthorizedAccessException("Usuário ou senha inválidos");

            return _tokenService.GenerateToken(user.Id, user.Email);

        }

        public async Task<UserCreateOutputDto> CreateUserAsync(UserCreateInputDto input)
        {
            var user = await _userManager.FindByEmailAsync(input.Email);

            if (user is not null)
                throw new ConflictException("Usuário já cadastrado com esse email!");

            var entity = new UserEntity
            {
                Email = input.Email,
                UserName = input.Email,
                CreatedAt = _datetimeUtilsService.GetNow(),
                Enabled = true
            };

            var response = await _userManager.CreateAsync(entity, input.Password);

            if (!response.Succeeded)
                throw new InvalidOperationException(response.Errors.First().Description);

            return new UserCreateOutputDto
            {
                Id = entity.Id,
                Email = entity.Email,
                Role = entity.Role
            };
        }

        public async Task<UserGetOutputDto> UpdateUserAsync(UserEntity user)
        {
            await _userManager.UpdateAsync(user);

            return new UserGetOutputDto
            {
                Id = user.Id,
                Email = user.Email,
                Enabled = user.Enabled,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                DeactivatedAt = user.DeactivatedAt,
                EnabledAt = user.EnabledAt,
                DeactivatedByEmail = user.DeactivatedByEmail,
                EnabledByEmail = user.EnabledByEmail
            };
        }

        public async Task<UserGetOutputDto> ChangeUserRole(string id, UserChangeRoleInputDto input)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user is null)
                throw new NotFoundException("Usuário não localizado");

            ValidateRole(input.Role);

            user.Role = input.Role;

            await _userManager.UpdateAsync(user);
            return GetUserOutputDto(user);
        }

        public async Task DisableUser(string userId, string callerEmail)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                throw new NotFoundException("Usuário não localizado com esse email");

            if (!user.Enabled)
                throw new InvalidOperationException("Usuário já inativo");

            user.Enabled = false;
            user.DeactivatedAt = _datetimeUtilsService.GetNow();
            user.DeactivatedByEmail = callerEmail;

            await _userManager.UpdateAsync(user);
        }

        public async Task EnableUser(string userId, string callerEmail)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                throw new NotFoundException("Usuário não localizado com esse email");

            if (user.Enabled)
                throw new InvalidOperationException("Usuário já ativo");

            user.Enabled = true;
            user.EnabledAt = _datetimeUtilsService.GetNow();
            user.EnabledByEmail = callerEmail;

            await _userManager.UpdateAsync(user);
        }

        public async Task<UserGetOutputDto?> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                return null;

            return GetUserOutputDto(user);
        }

        public async Task<UserGetOutputDto?> GetUserByIdAndRoleAsync(string userId, ERole role)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                return null;

            if (!user.Role.Equals(nameof(ERole.Tecnico)))
            {
                return null;
            }

            return GetUserOutputDto(user);
        }

        public async Task<UserGetOutputDto?> GetByEmail(string username)
        {
            var user = await _userManager.FindByEmailAsync(username);

            if (user is null)
                return null;

            return GetUserOutputDto(user);

        }

        public async Task<IEnumerable<UserGetOutputDto>> GetAllUsersAsync()
        {
            // Retorna a lista de todos os usuários
            return await Task.FromResult(_userManager.Users
                    .ToList()
                    .Select(GetUserOutputDto));
        }

        private void ValidateRole(string role)
        {
            var roles = _rolesConfig.Roles.Any(r => r.Name.Equals(role, StringComparison.OrdinalIgnoreCase));

            if (!roles)
                throw new NotFoundException("Perfil não localizado");
        }

        private static UserGetOutputDto GetUserOutputDto(UserEntity? user)
        {
            return new UserGetOutputDto
            {
                Id = user.Id,
                Email = user.Email,
                Enabled = user.Enabled,
                Role = user.Role,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                DeactivatedAt = user.DeactivatedAt,
                EnabledAt = user.EnabledAt,
                DeactivatedByEmail = user.DeactivatedByEmail,
                EnabledByEmail = user.EnabledByEmail
            };
        }

    }
}
