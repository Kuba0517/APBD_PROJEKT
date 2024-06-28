using APBD_PROJEKT.RequestModels;
using FluentValidation;

namespace APBD_PROJEKT.Validators;

public class RegisterValidators: AbstractValidator<RegisterRequestModel>
{
    public RegisterValidators()
    {
        RuleFor(login => login.Login)
            .NotEmpty().WithMessage("Login is required.")
            .MaximumLength(128).WithMessage("Login must contain max 128 letters");

        RuleFor(login => login.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");
    }
    
}