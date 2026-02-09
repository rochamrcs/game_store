namespace GameStore.Api.Dtos;

// Um DTO é um contrato entre o client e o Servidor since it pois representa
// um acordo compartilhado sobre como os dados serão transferidos e usados


public record GameDetailsDto(
    int id,
    string Name,
    int GenreId,
    decimal Price,
    DateOnly ReleaseDate
);