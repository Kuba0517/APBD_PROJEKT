using APBD_PROJEKT.Models;
using APBD_PROJEKT.RequestModels;
using FluentValidation;

namespace APBD_PROJEKT.Validators;

public class CreateContractValidators: AbstractValidator<ContractRequestModel>
{
    public CreateContractValidators()
    {
        RuleFor(c => c.ClientId)
            .NotEmpty().WithMessage("ClientId is required.");

        RuleFor(c => c.SoftwareId)
            .NotEmpty().WithMessage("SoftwareId is required.");

        RuleFor(c => c.SoftwareVersion)
            .NotEmpty().WithMessage("SoftwareVersion is required.")
            .MaximumLength(50).WithMessage("SoftwareVersion must not exceed 50 characters.");

        RuleFor(c => c.StartDate)
            .NotEmpty().WithMessage("StartDate is required.")
            .Must((c, startDate) => startDate < c.EndDate)
            .WithMessage("StartDate must be before EndDate.");

        RuleFor(c => c.EndDate)
            .NotEmpty().WithMessage("EndDate is required.")
            .Must((c, endDate) => endDate > c.StartDate)
            .WithMessage("EndDate must be after StartDate.")
            .Must((c, endDate) => (endDate - c.StartDate).Days >= 3)
            .WithMessage("The difference between StartDate and EndDate must be at least 3 days.")
            .Must((c, endDate) => (endDate - c.StartDate).Days <= 30)
            .WithMessage("The difference between StartDate and EndDate must be no more than 30 days.");

        RuleFor(c => c.Price)
            .NotEmpty().WithMessage("Price is required and cannot be 0.")
            .GreaterThan(0).WithMessage("Price must be greater than 0.");

        RuleFor(c => c.SupportYears)
            .InclusiveBetween(1, 3).WithMessage("SupportYears must be between 1 and 3.");

        RuleFor(c => c.IsSigned)
            .Must(isSigned => isSigned is true or false)
            .WithMessage("IsSigned must be either true or false.");
    }
}