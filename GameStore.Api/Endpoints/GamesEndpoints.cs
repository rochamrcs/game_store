using CreateGameStore.Api.Dtos;
using GameStore.Api.Dtos;
using UpdateGameStore.Api.Dtos;

namespace GameStore.Api.Endpoints;

public static class GamesEndPoints
{
    const string GetGameEndpointName = "GetGame";
    private static readonly List<GameDto> games = [
    new (1, "Pokemon Silver", "RPG", 79.99M, new DateOnly(1999, 11, 21)),
    new (2, "The Legend of Zelda: Ocarina of Time", "Adventure", 99.99M, new DateOnly(1998, 11, 21)),
    new (3, "Super Mario World", "Platform", 59.99M, new DateOnly(1990, 11, 21)),
    new (4, "Final Fantasy VII", "RPG", 89.99M, new DateOnly(1997, 1, 31)),
    new (5, "Metal Gear Solid", "Action", 69.99M, new DateOnly(1998, 9, 3)),
    ];

    public static void MapGamesEndPoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");

        group.MapGet("/", () => games);

        group.MapGet("/{id}", (int id) =>
        {
            var game = games.Find(games => games.id == id);

            return game is null ? Results.NotFound() : Results.Ok(game);
        })
        .WithName(GetGameEndpointName);

        group.MapPost("/", (CreateGameDto newGame) => {
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

        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            var index = games.FindIndex(game => game.id == id);

            if (index == -1)
            {
                return Results.NotFound();
            };

            games[index] = new GameDto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );

            return Results.NoContent();
        });

        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.id == id);

            return Results.NoContent();

        });
    }

}
