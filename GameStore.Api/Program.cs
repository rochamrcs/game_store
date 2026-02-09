using GameStore.Api.Data;
using GameStore.Api.Endpoints;
using GameStore.Api.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();

builder.AddGameStoreDb();

var app = builder.Build();

app.MapGet("/", () => "Hello, Wolrd!");

app.MapGamesEndPoints();
app.MapGenresEndpoint();

app.MigrateDb();

app.Run();
