using Services.Abstractions;
using Services;
using Domain.Repositories;
using PostgresDatabase.Repositories;
using PostgresDatabase.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using ExternalProcesses;
using Services.Abstractions.Generic;

var builder = WebApplication.CreateBuilder();
#if DEBUG
var conString = builder.Configuration.GetConnectionString("MyPostgres");
#else
var conString = builder.Configuration.GetConnectionString("Postgres");
#endif

// Add services to the container
builder.Services.AddHostedService<ExternalProcessManager>();
builder.Services.AddTransient<IProcessHandler, ProcessService>();

builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddScoped<IServiceManager, ServiceManager>();

builder.Services.AddDbContext<PostgresDbContext>(builder =>
{
    builder
    .UseNpgsql(conString, x => x.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "efcore"))
    .UseSnakeCaseNamingConvention();
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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
