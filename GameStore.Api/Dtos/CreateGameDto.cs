using System.ComponentModel.DataAnnotations;

namespace CreateGameStore.Api.Dtos;

public record CreateGameDto(
    int id,
    [Required][StringLength(50)] string Name,
    [Required][StringLength(20)] string Genre,
    [Required][Range(1,250)] decimal Price,
    DateOnly ReleaseDate
);
