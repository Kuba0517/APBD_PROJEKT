using APBD_PROJEKT.Helpers.Enums;
using APBD_PROJEKT.RequestModels;
using FluentValidation;

namespace APBD_PROJEKT.Validators;

public class CreatePaymentValidators: AbstractValidator<ContractPaymentRequestModel>
{
    public CreatePaymentValidators()
    {
        RuleFor(p => p.ContractId)
            .NotEmpty().WithMessage("ContractId is required.");

        RuleFor(p => p.Value)
            .GreaterThan(0).WithMessage("Value must be greater than 0.");

        RuleFor(p => p.PaymentType)
            .Must(type => type == PaymentType.Monthly)
            .Must(type => type == PaymentType.Single)
            .WithMessage("PaymentType must be either 0 or 1.");
    }
}