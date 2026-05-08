using Orders.API;
using Orders.Application;
using Orders.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddApiServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApplicationServices();

app.MapGet("/", () => "Hello World!");

app.Run();
