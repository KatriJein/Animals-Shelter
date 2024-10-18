
using AnimalsShelterBackend;
using AnimalsShelterBackend.Infrastructure;
using AnimalsShelterBackend.Infrastructure.Configurations;
using AnimalsShelterBackend.Startups.Animals;
using Core;
using Core.MinIO;
using Core.Serilog;
using Microsoft.EntityFrameworkCore;
using Minio;
using Minio.DataModel.Args;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Serilog;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.AddSerilog();
builder.Services.AddNewControllers();
builder.Services.ConfigureDbConnection(builder.Configuration);
builder.Services.ConfigureMinio(builder.Configuration);
builder.Services.AddMinIOStorage(builder.Configuration);
builder.Services.AddDbContext<ShelterAppContext>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAnimalsDomain();
builder.Services.AddAnimalsServices();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddNewSwaggerGen();
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
