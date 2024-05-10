namespace KolokwiumApp.DTOs;

public record GetBookDto(
    int Id,
    string Title,
    List<string?> Genres
);