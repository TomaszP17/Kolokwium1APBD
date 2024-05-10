namespace KolokwiumApp.DTOs;

public record AddBookDto(
    string Title,
    List<int> Genres
);