var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCarter();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();

var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssembly(assembly);
    options.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    options.AddOpenBehavior(typeof(LoggingBehavior<,>));


});

builder.Services.AddMarten(options => 
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
    options.Schema.For<ShoppingCart>().Identity(x => x.UserName);
})
    .UseLightweightSessions();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline
app.MapCarter();
app.UseExceptionHandler(opt => { });
app.Run();
