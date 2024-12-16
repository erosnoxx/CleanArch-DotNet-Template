using Microsoft.AspNetCore.Identity;

namespace Application.Contracts.Services
{
    public interface IRoleService
    {
        Task<IdentityResult> CreateRoleAsync(IdentityRole role);
        Task<IdentityResult> UpdateRoleAsync(IdentityRole role);
        Task<IdentityResult> DeleteRoleAsync(IdentityRole role);
        Task<IdentityRole?> GetRoleByIdAsync(string roleId);
        Task<IdentityRole?> GetRoleByNameAsync(string name);
        Task<IEnumerable<IdentityRole>> GetAllRolesAsync();
    }
}
