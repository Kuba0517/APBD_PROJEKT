using APBD_PROJEKT.RequestModels;
using FluentValidation;

namespace APBD_PROJEKT.Validators;

public class LoginValidators : AbstractValidator<LoginRequestModel>
{
    public LoginValidators()
    {
        RuleFor(login => login.Login)
            .NotEmpty().WithMessage("Login is required.");

        RuleFor(login => login.Password)
            .NotEmpty().WithMessage("Password is required.");
    }
}
