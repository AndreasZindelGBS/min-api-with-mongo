using MongoDB.Driver;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
var movieDatabaseConfigSection = builder.Configuration.GetSection("DatabaseSettings");
builder.Services.Configure<DatabaseSettings>(movieDatabaseConfigSection);

var app = builder.Build();

app.MapGet("/", () =>
{
    return "Minimal API Version 1.0";
});

app.MapGet("/check", (IOptions<DatabaseSettings> options) =>
{
    try
    {
        var mongoDbConnectionString = options.Value.ConnectionString;
        var client = new MongoClient(mongoDbConnectionString);
        var databases = client.ListDatabaseNames().ToList();
        return $"Datenbanken: {string.Join(", ", databases)}";
    }
    catch (Exception ex)
    {
        return $"Fehler: {ex.Message}";
    }
});


// Insert Movie
app.MapPost("/api/movies", (Movie movie) =>
{
    throw new NotImplementedException();
});

// Get all Movies
app.MapGet("/api/movies", () =>
{
    throw new NotImplementedException();
});

// Get Movie by id
app.MapGet("/api/movies/{id}", (string id) =>
{
    throw new NotImplementedException();
});

// Update Movie
app.MapPut("/api/movies/{id}", (string id, Movie movie) =>
{
    throw new NotImplementedException();
});

// Delete Movie
app.MapDelete("/api/movies/{id}", (string id) =>
{
    throw new NotImplementedException();
});

// WICHTIG: ganz nach unten verschoben
app.Run();
