using GameStore.Api.Dtos;

namespace GameStore.Api.Endpoints;

public static class GamesEndPoints
{
    List<GameDto> games = [
    new (1, "Pokemon Silver", "RPG", 79.99M, new DateOnly(1999, 11, 21)),
    new (2, "The Legend of Zelda: Ocarina of Time", "Adventure", 99.99M, new DateOnly(1998, 11, 21)),
    new (3, "Super Mario World", "Platform", 59.99M, new DateOnly(1990, 11, 21)),
    new (4, "Final Fantasy VII", "RPG", 89.99M, new DateOnly(1997, 1, 31)),
    new (5, "Metal Gear Solid", "Action", 69.99M, new DateOnly(1998, 9, 3)),
];
}
