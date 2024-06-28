using System.Text.RegularExpressions;
using APBD_PROJEKT.RequestModels;
using FluentValidation;

namespace APBD_PROJEKT.Validators;

public class CreateClientValidators: AbstractValidator<ClientRequestModel>
{
    public CreateClientValidators()
    {
        RuleFor(e => e)
            .Must(HaveValidClientType).WithMessage("You can either input individual client or comapny.");
        
        RuleFor(e => e.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required")
            .MinimumLength(9).WithMessage("PhoneNumber must not be less than 9 characters.")
            .MaximumLength(20).WithMessage("PhoneNumber must not exceed 20 characters.")
            .Matches(new Regex(@"^(\+48)?[ -]?(\d{3}[ -]?\d{3}[ -]?\d{3})$")).WithMessage("PhoneNumber not valid");

        RuleFor(e => e.Email)
            .NotEmpty().WithMessage("Email address is required")
            .EmailAddress().WithMessage("A valid email address is required.")
            .MaximumLength(128).WithMessage("Maximum Name length is 57");

        RuleFor(e => e.Address)
            .NotEmpty().WithMessage("Address is required")
            .MaximumLength(255).WithMessage("Maximum Address length is 255");

        When(client => !string.IsNullOrEmpty(client.CompanyName) && !string.IsNullOrEmpty(client.Krs), () =>
        {
            RuleFor(e => e.CompanyName)
                .NotEmpty().WithMessage("Company name is required.")
                .MaximumLength(255).WithMessage("Maximum CompanyName length is 255");
            
            RuleFor(e => e.Krs)
                .NotEmpty().WithMessage("KRS is required.")
                .Matches(@"^\d{10}$").WithMessage("KRS must contain exactly 10 digits.");
        });
        
        When(client => !string.IsNullOrEmpty(client.Name) && !string.IsNullOrEmpty(client.Surname) && !string.IsNullOrEmpty(client.Pesel), () =>
        {
            RuleFor(client => client.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(57).WithMessage("Maximum Name length is 57");
            
            RuleFor(client => client.Surname)
                .NotEmpty().WithMessage("Surname is required.")
                .MaximumLength(36).WithMessage("Maximum Surname length is 36");;
            
            RuleFor(client => client.Pesel)
                .NotEmpty().WithMessage("PESEL is required.")
                .Matches(@"^\d{11}$").WithMessage("PESEL must contain exactly 11 digits.");
        });
    }
    
    private bool HaveValidClientType(ClientRequestModel client)
    {
        var hasCompanyData = !string.IsNullOrEmpty(client.CompanyName) && !string.IsNullOrEmpty(client.Krs);
        var hasIndividualData = !string.IsNullOrEmpty(client.Name) && !string.IsNullOrEmpty(client.Surname) && !string.IsNullOrEmpty(client.Pesel);

        return !(hasCompanyData && hasIndividualData);
    }
}