using System.Reflection;
using Microsoft.OpenApi.Models;
using PokerAlgoAPI.Endpoints;
using PokerAlgoAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "PokerAlgoAPI",
        Description = "An ASP.NET Core Web Minmal API for evaluating hands and determining the winner of a Texas Hold 'em Game.",
        Contact = new OpenApiContact
        {
            Name = "PokerAlgo GitHub",
            Url = new Uri("https://github.com/holypeachy/PokerAlgo-WebAPI")
        }
    });
    string xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

// Services
builder.Services.AddScoped<IPokerAlgoService, PokerAlgoServices>();

var app = builder.Build();
app.UseStaticFiles();

// Enable Swagger
app.UseSwagger();
app.UseSwaggerUI( options =>
{
    options.DocumentTitle = "PokerAlgo API Docs";
    options.InjectStylesheet("/css/swagger-custom.css");
});

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
