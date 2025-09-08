using PokerAlgoAPI.Endpoints;
using PokerAlgoAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
});

// Services
builder.Services.AddScoped<IPokerAlgoService, PokerAlgoServices>();

var app = builder.Build();

// Enable Swagger
app.UseSwagger();
app.UseSwaggerUI();

// Root to swagger for devs :3
app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

// Map my endpoints
PokerAlgoEndPoints.MapEndPoints(app);

// Meow
app.Run();
