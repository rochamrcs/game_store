using CreateGameStore.Api.Dtos;
using GameStore.Api.Dtos;
using UpdateGameStore.Api.Dtos;

const string GetGameEndpointName = "GetGame";

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


List<GameDto> games = [
    new (1, "Pokemon Silver", "RPG", 79.99M, new DateOnly(1999, 11, 21)),
    new (2, "The Legend of Zelda: Ocarina of Time", "Adventure", 99.99M, new DateOnly(1998, 11, 21)),
    new (3, "Super Mario World", "Platform", 59.99M, new DateOnly(1990, 11, 21)),
    new (4, "Final Fantasy VII", "RPG", 89.99M, new DateOnly(1997, 1, 31)),
    new (5, "Metal Gear Solid", "Action", 69.99M, new DateOnly(1998, 9, 3)),
];

app.MapGet("/", () => "Hello, Wolrd!");

app.MapGet("/games", () => games);

app.MapGet("/games/{id}", (int id) =>
{
    var game = games.Find(games => games.id == id);

    return game is null ? Results.NotFound() : Results.Ok(game);
})
.WithName(GetGameEndpointName);

app.MapPost("/games", (CreateGameDto newGame) => {
    GameDto game = new(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );

    games.Add(game);

    return Results.CreatedAtRoute(GetGameEndpointName, new {id = game.id}, game);

});

app.MapPut("/games/{id}", (int id, UpdateGameDto updatedGame) =>
{
    var index = games.FindIndex(game => game.id == id);

    games[index] = new GameDto(
        id,
        updatedGame.Name,
        updatedGame.Genre,
        updatedGame.Price,
        updatedGame.ReleaseDate
    );

    return Results.NoContent();
});

app.MapDelete("/games/{id}", (int id) =>
{
    games.RemoveAll(game => game.id == id);

    return Results.NoContent();

});

app.Run();
