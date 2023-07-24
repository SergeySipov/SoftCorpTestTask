using Application;
using Infrastructure;
using Persistence;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;
configuration.AddUserSecrets(Assembly.GetExecutingAssembly(), true);
var webHostEnvironment = builder.Environment;


// Add services to the container.
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddApplicationServices();
services.AddInfrastructureServices(configuration);
services.AddPersistentServices(configuration);


// Configure the HTTP request pipeline.
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();


app.Run();