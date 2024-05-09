using FluentValidation;
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
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> AddBook(AddBookDto bookDto, IValidator<AddBookDto> validator)
    {
        return Ok();
    }
}