using System.ComponentModel.DataAnnotations;

namespace UpdateGameStore.Api.Dtos;

public record UpdateGameDto(
    [Required][StringLength(50)] string Name,
    [Range(1, 50)] int GenreId,
    [Required][Range(1,250)] decimal Price,
    DateOnly ReleaseDate
);
