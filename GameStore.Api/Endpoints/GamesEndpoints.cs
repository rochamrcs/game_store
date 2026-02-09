using CreateGameStore.Api.Dtos;
using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;
using UpdateGameStore.Api.Dtos;

namespace GameStore.Api.Endpoints;

public static class GamesEndPoints
{
    const string GetGameEndpointName = "GetGame";
    private static readonly List<GameSummaryDto> games = [
        new (1, "Pokemon Silver", "RPG", 79.99M, new DateOnly(1999, 11, 21)),
        new (2, "The Legend of Zelda: Ocarina of Time", "Adventure", 99.99M, new DateOnly(1998, 11, 21)),
        new (3, "Super Mario World", "Platform", 59.99M, new DateOnly(1990, 11, 21)),
        new (4, "Final Fantasy VII", "RPG", 89.99M, new DateOnly(1997, 1, 31)),
        new (5, "Metal Gear Solid", "Action", 69.99M, new DateOnly(1998, 9, 3)),
    ];

    public static void MapGamesEndPoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");

        group.MapGet("/",async (GameStoreContext dbContext) 
        => await dbContext.Games
                          .Include(game => game.Genre)
                          .Select(game => new GameSummaryDto(
                            game.Id,
                            game.Name,
                            game.Genre!.Name,
                            game.Price,
                            game.ReleaseDate
                        ))
                        .AsNoTracking()
                        .ToListAsync());

        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            var game = await dbContext.Games.FindAsync(id);

            return game is null ? Results.NotFound() : Results.Ok(
                new GameDetailsDto(
                    game.Id,
                    game.Name,
                    game.GenreId,
                    game.Price,
                    game.ReleaseDate
                )
            );
        })
        .WithName(GetGameEndpointName);

        group.MapPost("/", async (CreateGameDto newGame, GameStoreContext dbContext) => {

            Game game = new()
            {
                Name = newGame.Name,
                GenreId = newGame.GenreId,
                Price = newGame.Price,
                ReleaseDate = newGame.ReleaseDate
            };

            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

            GameDetailsDto gameDto = new(
                game.Id,
                game.Name,
                game.GenreId,
                game.Price,
                game.ReleaseDate
            );

            return Results.CreatedAtRoute(GetGameEndpointName, new {id = gameDto.id}, gameDto);

        });

        group.MapPut("/{id}", async (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) =>
        {
            var existingGame = await dbContext.Games.FindAsync(id);

            if (existingGame is null)
            {
                return Results.NotFound();
            };

            existingGame.Name = updatedGame.Name;
            existingGame.GenreId = updatedGame.GenreId;
            existingGame.Price = updatedGame.Price;
            existingGame.ReleaseDate= updatedGame.ReleaseDate;

            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            await dbContext.Games
                    .Where(game => game.Id == id)
                    .ExecuteDeleteAsync();

            return Results.NoContent();

        });
    }

}
