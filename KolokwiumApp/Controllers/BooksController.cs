using FluentValidation;
using KolokwiumApp.DTOs;
using KolokwiumApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace KolokwiumApp.Controllers;

[ApiController]
[Route("/api/books")]
public class BooksController(IDbService db) : ControllerBase
{
    [HttpGet("{id:int}/genres")]
    public async Task<IActionResult> GetBookByGenres(int id)
    {
        //pobranie danych z bazy jak null to nie znaleziono inaczej ok
        var result = await db.GetBookById(id);
        if (result == null) return NotFound($"The book with id: {id} is not exists");
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddBook(AddBookDto bookDto, IValidator<AddBookDto> validator)
    {
        //walidacja
        var validate = await validator.ValidateAsync(bookDto);

        if (!validate.IsValid)
        {
            return ValidationProblem(); 
        }
        //sprawdzanie czy dany datuenk istnieje w bazie 
        foreach (var genre in bookDto.Genres)
        {
            if (!await db.GetGenresById(genre))
            {
                return BadRequest($"Genre with that id: {genre} is not exists");
            }
        }
        //sprawdzanie czy problem wystapil podczas insertowania do bazy
        var result = await db.AddBookAsync(bookDto);
        if (result == -1)
        {
            return StatusCode(500, bookDto);
        }
        //zwrocenie informacji o dodanym ksiazce
        var createdBook = await db.GetBookById(result);
        return Created($"/api/books/{result}/genres", createdBook);
    }
}