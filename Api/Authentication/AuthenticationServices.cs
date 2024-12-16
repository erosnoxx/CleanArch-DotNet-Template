using Application.Contracts.Settings;
using Data.Context;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Api.Authentication
{
    public static class AuthenticationServices
    {
        public static IServiceCollection AddAuthenticationServices(this IServiceCollection services)
        {
            var appSettingsService = services.BuildServiceProvider().GetService<IAppSettings>();

            services.AddIdentityCore<UserEntity>()
                .AddEntityFrameworkStores<DatabaseContext>()
                .AddTokenProvider<DataProtectorTokenProvider<UserEntity>>("Default");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = appSettingsService?.JwtSettings.Issuer,
                        ValidAudience = appSettingsService?.JwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettingsService?.JwtSettings.SecretKey))
                    };
                });

            services.Configure<IdentityOptions>(options =>
            {
                // Configuração de senha
                //options.Password.RequireDigit = true;
                //options.Password.RequireLowercase = true;
                //options.Password.RequireNonAlphanumeric = false;
                //options.Password.RequireUppercase = true;
                //options.Password.RequiredLength = 6;
                //options.Password.RequiredUniqueChars = 1;
                options.Password.RequireDigit = false;           // Não exige dígitos
                options.Password.RequireLowercase = false;       // Não exige letras minúsculas
                options.Password.RequireNonAlphanumeric = false; // Não exige caracteres não alfanuméricos
                options.Password.RequireUppercase = false;       // Não exige letras maiúsculas
                options.Password.RequiredLength = 1;             // Comprimento mínimo da senha (ajustar conforme necessário)
                options.Password.RequiredUniqueChars = 1;        // Número mínimo de caracteres únicos (ajustar conforme necessário)


                // Configuração de bloqueio
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // Configuração de usuário
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });

            return services;
        }
    }
}
