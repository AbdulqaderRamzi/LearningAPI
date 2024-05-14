using System.ComponentModel.DataAnnotations;

namespace GameStore.Dtos;

public record class CreateGameDto(
    [Required] string Name,
    [Required] string Genre,
    [Required] decimal Price,
    [Required] DateOnly ReleasedDate
);
