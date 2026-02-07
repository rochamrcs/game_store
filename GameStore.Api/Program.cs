using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.MapGet("/", () => "Hello, Wolrd!");

app.MapGamesEndPoints();

app.Run();
