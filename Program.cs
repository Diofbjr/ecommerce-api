using Ecommerce.Api.Application.Addresses.Services;
using Ecommerce.Api.Application.Auth.Services;
using Ecommerce.Api.Application.Orders.Services;
using Ecommerce.Api.Application.Users.Services;
using Ecommerce.Api.Infrastructure.Data;
using Ecommerce.Api.Infrastructure.Repositories;
using Ecommerce.Api.Infrastructure.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ----------------------------------------------------------
// 🔥 1. Carregar configurações (Render usa variáveis env)
// ----------------------------------------------------------
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

// ----------------------------------------------------------
// 🔥 2. Banco de Dados - POSTGRESQL (Render usa env)
// ----------------------------------------------------------
var connectionString = builder.Configuration.GetConnectionString("Default");

if (string.IsNullOrEmpty(connectionString))
{
    Console.WriteLine("❌ ERRO: ConnectionStrings__Default não foi definida!");
}
else
{
    Console.WriteLine("✅ ConnectionString carregada com sucesso.");
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString) // ← ALTERADO PARA POSTGRES
);

// ----------------------------------------------------------
// 🔥 3. Registrar serviços e repositórios
// ----------------------------------------------------------
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderTrackingService, OrderTrackingService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<ITokenService, JwtTokenService>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

// ----------------------------------------------------------
// 🔥 4. Controllers + Swagger
// ----------------------------------------------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ----------------------------------------------------------
// 🔥 5. Pipeline
// ----------------------------------------------------------
app.UseSwagger();
app.UseSwaggerUI();

// Render precisa permitir todas as origens (opcional ativar)
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.MapControllers();

app.Run();
