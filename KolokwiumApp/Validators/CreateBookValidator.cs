using FluentValidation;
using KolokwiumApp.DTOs;

namespace KolokwiumApp.Validators;

public class CreateBookValidator : AbstractValidator<AddBookDto>
{
    public CreateBookValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("Title Book must be 1-100 lenght");
    }
}