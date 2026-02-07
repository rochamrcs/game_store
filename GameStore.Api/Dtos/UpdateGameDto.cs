using System.ComponentModel.DataAnnotations;

namespace UpdateGameStore.Api.Dtos;

public record UpdateGameDto(
    [Required][StringLength(50)] string Name,
    [Required][StringLength(20)] string Genre,
    [Required][Range(1,250)] decimal Price,
    DateOnly ReleaseDate
);
