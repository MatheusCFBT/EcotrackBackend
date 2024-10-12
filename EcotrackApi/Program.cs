using Ecotrack.Context;
using EcotrackBusiness.Interfaces;
using EcotrackBusiness.Models;
using EcotrackBusiness.Notifications;
using EcotrackBusiness.Services;
using EcotrackData.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EcotrackDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<EcotrackDbContext>();
builder.Services.AddScoped<IRepository<Cliente>, Repository<Cliente>>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<INotificador, Notificador>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
