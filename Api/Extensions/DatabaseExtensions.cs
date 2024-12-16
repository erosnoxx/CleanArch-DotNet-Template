using Application.Contracts.Services;
using Application.Contracts.Settings;
using Application.Enums;
using Application.Models.Dtos.User.Create;
using Application.Models.Dtos.User.Update;
using Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Api.Extensions
{
    public static class DatabaseExtensions
    {
        public static IApplicationBuilder MigrateDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DatabaseContext>();
                if (context != null && context.Database != null)
                {
                    context.Database.Migrate();
                }
            }
            return app;
        }

        public static async Task<IApplicationBuilder> CreateDefaultAdmin(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var appSettings = serviceScope.ServiceProvider.GetService<IAppSettings>();
                var userService = serviceScope.ServiceProvider.GetService<IUserService>();

                if (userService is null || appSettings is null)
                {
                    throw new ArgumentException("UserService or RoleService or Appsettings cannot be null");
                }

                var roleName = nameof(ERole.Admin);
                var email = appSettings.AdminSettings.Email;
                var password = appSettings.AdminSettings.Password;

                UserCreateOutputDto? userCreated = null;
                var user = await userService.GetByEmail(email);
                if (user is null)
                {
                    var userCreate = new UserCreateInputDto
                    {
                        Email = email,
                        Password = password,
                    };

                    userCreated = await userService.CreateUserAsync(userCreate);

                }

                if (
                    userCreated is not null && !userCreated.Role.Equals(nameof(ERole.Admin)) ||
                    user is not null && !user.Role.Equals(nameof(ERole.Admin))
                )
                {
                    var id = userCreated is not null ? userCreated.Id : user.Id;

                    await userService.ChangeUserRole(id, new UserChangeRoleInputDto
                    {
                        Role = roleName
                    });
                }
            }
            return app;
        }
    }
}
