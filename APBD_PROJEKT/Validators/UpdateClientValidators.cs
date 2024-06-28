using System.Text.RegularExpressions;
using APBD_PROJEKT.DTOs;
using FluentValidation;

namespace APBD_PROJEKT.Validators;

public class UpdateClientValidators : AbstractValidator<ClientUpdateDto>
{
    public UpdateClientValidators()
    {
        RuleFor(e => e.PhoneNumber)
            .MinimumLength(10).WithMessage("PhoneNumber must not be less than 10 characters.")
            .MaximumLength(20).WithMessage("PhoneNumber must not exceed 50 characters.")
            .Matches(new Regex(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}")).WithMessage("PhoneNumber not valid");

        RuleFor(e => e.Email)
            .EmailAddress().WithMessage("A valid email address is required.");

        RuleFor(e => e.Name)
            // fun fact najdłuższe imie na świecie ma 57 liter
            .MaximumLength(57);

        RuleFor(e => e.Surname)
            // najdłuższe nazwisko na świecie - 36 liter
            .MaximumLength(36);
    }
}