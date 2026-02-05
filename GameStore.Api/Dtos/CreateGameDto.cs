namespace CreateGameStore.Api.Dtos;

public record CreateGameDto(
    int id,
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);
