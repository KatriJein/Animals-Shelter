
using AnimalsShelterBackend.Domain.Animals;
using AnimalsShelterBackend.Infrastructure;
using AnimalsShelterBackend.Infrastructure.Configurations;
using Core;
using Core.Serilog;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.AddSerilog();
builder.Services.AddControllers();
builder.Services.ConfigureDbConnection(builder.Configuration);
builder.Services.AddDbContext<ShelterAppContext>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapGet("/api/animals", async (ShelterAppContext dbContext) =>
{
	return await dbContext.Animals.ToListAsync();
});
app.MapPost("api/animals", async (ShelterAppContext dbContext) =>
{
	var animal = new Animal() { Name = "Vasya" };
	await dbContext.Animals.AddAsync(animal);
	await dbContext.SaveChangesAsync();
});
app.Run();
