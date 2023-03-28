using DataAccessLayer.Context;
using ServerioAPI.Interfaces;
using ServerioAPI.Services;
using ServerioAPI.Services.Information;

var builder = WebApplication.CreateBuilder();

// Add services to the container

builder.Services.AddScoped<IProcessService, ProcessService>();
builder.Services.AddScoped<IGamesService, GamesInfoService>();

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
