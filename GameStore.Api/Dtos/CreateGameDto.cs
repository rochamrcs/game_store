using System.ComponentModel.DataAnnotations;

namespace CreateGameStore.Api.Dtos;

public record CreateGameDto(
    int id,
    [Required][StringLength(50)] string Name,
    [Range(1, 50)] int GenreId,
    [Required][Range(1,250)] decimal Price,
    [Required] DateOnly ReleaseDate
);
