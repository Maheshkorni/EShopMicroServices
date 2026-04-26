using Discount.GrpcService.Data;
using Discount.GrpcService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

var Sqlite_Config = builder.Configuration.GetSection("SQLite_Config");
var workingDir = Environment.GetEnvironmentVariable("IS_DOCKER") == null ? Directory.GetCurrentDirectory() : "";
Directory.CreateDirectory(workingDir + "/" +Sqlite_Config["DB_FOLDER"]);

var connectionString = $"Data Source={workingDir}/{Sqlite_Config["DB_FOLDER"]}/{Sqlite_Config["DB_NAME"]}";
builder.Services.AddDbContext<DiscountDbContext>(options =>
        {
            options.UseSqlite(connectionString);
        });


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMigration();
app.MapGrpcService<DiscountService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
