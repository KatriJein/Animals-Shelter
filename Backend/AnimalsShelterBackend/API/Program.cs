
using AnimalsShelterBackend.Domain.Animals;
using AnimalsShelterBackend.Infrastructure;
using AnimalsShelterBackend.Infrastructure.Configurations;
using Core;
using Core.MinIO;
using Core.Serilog;
using Microsoft.EntityFrameworkCore;
using Minio;
using Minio.DataModel.Args;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.AddSerilog();
builder.Services.AddControllers();
builder.Services.ConfigureDbConnection(builder.Configuration);
builder.Services.ConfigureMinio(builder.Configuration);
builder.Services.AddMinIOStorage(builder.Configuration);
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
//app.UseHttpsRedirection();

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
app.MapGet("/api/minio", async (IMinioClient client) =>
{
	var bucketExistsArgs = new BucketExistsArgs().WithBucket("myanimals559lol7777");
	var doesExists = await client.BucketExistsAsync(bucketExistsArgs);
	Console.WriteLine(doesExists);
	var makeBucketArgs = new MakeBucketArgs().WithBucket("myanimals559lol7777");
	await client.MakeBucketAsync(makeBucketArgs);
	var buckets = await client.ListBucketsAsync();
	return buckets.Buckets;
});
app.Run();
