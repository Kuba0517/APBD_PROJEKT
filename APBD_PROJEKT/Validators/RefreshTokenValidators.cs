using APBD_PROJEKT.RequestModels;
using FluentValidation;

namespace APBD_PROJEKT.Validators;

public class RefreshTokenValidators: AbstractValidator<RefreshTokenRequestModel>
{
    public RefreshTokenValidators()
    {
        RuleFor(e => e.RefreshToken)
            .NotEmpty().WithMessage("Refresh Token is required");
    }
}