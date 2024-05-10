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
        var result = await db.GetBookById(id);
        if (result == null) return NotFound($"The book with id: {id} is not exists");
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddBook(AddBookDto bookDto, IValidator<AddBookDto> validator)
    {
        var validate = await validator.ValidateAsync(bookDto);

        if (!validate.IsValid)
        {
            return ValidationProblem(); 
        }

        foreach (var genre in bookDto.Genres)
        {
            if (!await db.GetGenresById(genre))
            {
                return BadRequest($"Genre with that id: {genre} is not exists");
            }
        }
        
        var result = await db.AddBookAsync(bookDto);
        if (result == -1)
        {
            return StatusCode(500, bookDto);
        }

        var createdBook = await db.GetBookById(result);
        return Created($"/api/books/{result}/genres", createdBook);
    }
}