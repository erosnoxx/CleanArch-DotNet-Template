using Api.Authentication;
using Api.Configurations;
using Data;
using Api.Extensions;
using Infrastructure;
using Api.Middlewares;
using System.Text.Json;
using Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
     .AddJsonOptions(options =>
     {
         options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
         options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
         options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
     });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddDataServices();
builder.Services.AddInfrastructureServices();

builder.Services.AddAuthenticationServices();
builder.Services.AddSwaggerConfigurationService();
builder.Services.AddRoleConfigurationService();
builder.Services.AddAuthorization();

// Adicione a configuração de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Template API v1");
});

app.UseHttpsRedirection();

// Aplicar a política de CORS
app.UseCors("AllowAllOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MigrateDatabase();
await app.CreateDefaultAdmin();

app.Run();
