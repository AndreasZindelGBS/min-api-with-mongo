using MongoDB.Driver;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("DatabaseSettings")
);

var app = builder.Build();

app.MapGet("/", () =>
{
    return "Minimal API Version 1.0";
});

app.MapGet("/check", (IOptions<DatabaseSettings> options) =>
{
    try
    {
        var client = new MongoClient(options.Value.ConnectionString);
        var databases = client.ListDatabaseNames().ToList();

        return Results.Ok(databases);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapPost("/api/movies", (Movie movie) =>
{
    movie.Id ??= Guid.NewGuid().ToString();
    return Results.Created($"/api/movies/{movie.Id}", movie);
});

app.MapGet("/api/movies", () =>
{
    return Results.Ok(new List<Movie>());
});

app.MapGet("/api/movies/{id}", (string id) =>
{
    return Results.Ok(new Movie { Id = id });
});

app.MapPut("/api/movies/{id}", (string id, Movie movie) =>
{
    movie.Id = id;
    return Results.Ok(movie);
});

app.MapDelete("/api/movies/{id}", (string id) =>
{
    return Results.Ok();
});

app.Run();