using Ecommerce.Api.Application.Addresses.Services;
using Ecommerce.Api.Application.Auth.Services;
using Ecommerce.Api.Application.Orders.Services;
using Ecommerce.Api.Application.Users.Services;
using Ecommerce.Api.Infrastructure.Data;
using Ecommerce.Api.Infrastructure.Repositories; // ← ADICIONAR
using Ecommerce.Api.Infrastructure.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração do banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// Registrar serviços
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderTrackingService, OrderTrackingService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<ITokenService, JwtTokenService>();

// REGISTRAR O REPOSITÓRIO ← RESOLVE O ERRO
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// AutoMapper e FluentValidation
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

// Controllers e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Pipeline
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();