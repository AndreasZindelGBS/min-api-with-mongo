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

app.Run();