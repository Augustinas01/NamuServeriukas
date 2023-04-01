using DataAccessLayer.Context;
using ServerioAPI.Interfaces;
using ServerioAPI.Services;
using ServerioAPI.Services.Information;
using Services.Abstractions;
using Services;
using Domain.Repositories;
using PostgresDatabase.Repositories;
using PostgresDatabase.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

var builder = WebApplication.CreateBuilder();

// Add services to the container

builder.Services.AddScoped<IProcessService, ProcessService>();
builder.Services.AddScoped<IGamesService, GamesInfoService>();

builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddDbContext<FactorioDbContext>(builder =>
{
    var conString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("Postgres");
    builder.UseNpgsql(conString, x => x.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "efcore"));

});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PostgresContext>();

var app = builder.Build();

app.UseCors(builder => builder
.AllowAnyHeader()
.AllowAnyMethod()
.AllowAnyOrigin());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Run();
