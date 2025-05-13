using src.Application.Services;
using src.Domain.Interfaces;
using src.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<AccountService>();

builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();

app.Run();