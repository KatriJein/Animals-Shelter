
using AnimalsShelterBackend;
using AnimalsShelterBackend.Infrastructure;
using AnimalsShelterBackend.Infrastructure.Configurations;
using AnimalsShelterBackend.Infrastructure.Infrastructure.Masstransit;
using AnimalsShelterBackend.Infrastructure.Startups;
using AnimalsShelterBackend.Infrastructure.Startups.Articles;
using AnimalsShelterBackend.Infrastructure.Startups.Contributors;
using AnimalsShelterBackend.Infrastructure.Startups.Feedbacks;
using AnimalsShelterBackend.Infrastructure.Startups.Notifications;
using AnimalsShelterBackend.Infrastructure.Startups.RefreshTokens;
using AnimalsShelterBackend.Infrastructure.Startups.Users;
using AnimalsShelterBackend.Infrastructure.Startups.Views;
using AnimalsShelterBackend.Masstransit;
using AnimalsShelterBackend.Middleware;
using AnimalsShelterBackend.Services.Users.Seeds;
using AnimalsShelterBackend.Startups;
using AnimalsShelterBackend.Startups.Animals;
using AnimalsShelterBackend.Startups.Images;
using Core;
using Core.Constants;
using Core.MinIO;
using Core.Redis;
using Core.Serilog;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using Minio;
using Minio.DataModel.Args;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Serilog;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var isDev = builder.Environment.IsDevelopment();

builder.Services.AppendCors(builder.Configuration);

builder.Host.AddSerilog();
builder.Services.AddNewControllers();
builder.Services.ConfigureDbConnection(builder.Configuration);
builder.Services.ConfigureMinio(builder.Configuration);
builder.Services.AddMinIOStorage(builder.Configuration);
builder.Services.AddRedis(builder.Configuration);
builder.Services.AddDbContext<ShelterAppContext>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddTokens();
builder.Services.AddContributors();

builder.Services.AddImagesServices();

builder.Services.AddAnimalsDomain();
builder.Services.AddAnimalsServices();

builder.Services.AddUsersDomain();
builder.Services.AddUsersService();

builder.Services.AddArticlesDomain();
builder.Services.AddArticlesServices();

builder.Services.AddArticleViewsSupport();
builder.Services.AddNotifications();
builder.Services.AddFeedbacks();


builder.Services.UseMasstransitRabbitMQ(builder.Configuration);
builder.Services.AddMasstransitAbstractionServices();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
if (isDev)
	builder.Services.AddNewSwaggerGen();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (isDev)
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseSerilogRequestLogging();

app.UseCors(Const.FrontendCORS);

app.UseMiddleware<AuthMiddleware>();

app.UseAuthorization();


app.MapControllers();

using (var scope = app.Services.CreateScope())
{
	var seedService = scope.ServiceProvider.GetRequiredService<ISeedService>();
	await seedService.CreateAdminUser();
}

app.Run();
