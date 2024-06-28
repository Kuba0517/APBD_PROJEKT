using APBD_PROJEKT.Contexts;
using APBD_PROJEKT.Exceptions;
using APBD_PROJEKT.Helpers.Enums;
using APBD_PROJEKT.Models;
using APBD_PROJEKT.RequestModels;
using APBD_PROJEKT.ResponseModels;
using Microsoft.EntityFrameworkCore;

namespace APBD_PROJEKT.Services.ContractService;

public class ContractService(DatabaseContext context) : IContractService
{
    public async Task<ContractResponseModel> CreateContract(ContractRequestModel contractRequestModel)
    {
        var timeSpan = contractRequestModel.EndDate - contractRequestModel.StartDate;
        if (timeSpan.Days is < 3 or > 30)
        {
            throw new InvalidDurationException("Given time duration is invalid");
        }

        if (!await context.Clients.AnyAsync(c => c.ClientId == contractRequestModel.ClientId))
        {
            throw new ClientNotFoundException($"Client with id: {contractRequestModel.ClientId} has not been found");
        }

        if (!await context.Softwares.AnyAsync(s => s.SoftwareId == contractRequestModel.SoftwareId))
        {
            throw new SoftwareNotFoundException(
                $"Software with id: {contractRequestModel.SoftwareId} has not been found");
        }

        var discount = await context.Discounts
            .Where(d => d.EndTime >= DateTime.Now)
            .OrderByDescending(d => d.Value)
            .FirstOrDefaultAsync();

        var discountValue = discount?.Value ?? 0;

        discountValue +=
            await context.Contracts.AnyAsync(c => c.ClientId == contractRequestModel.ClientId) ? 5 : 0;

        var initialPrice = contractRequestModel.Price;

        var newContract = new Contract()
        {
            ClientId = contractRequestModel.ClientId,
            SoftwareId = contractRequestModel.SoftwareId,
            StartDate = contractRequestModel.StartDate,
            EndDate = contractRequestModel.EndDate,
            Price = CalculatePayment(initialPrice, discountValue, contractRequestModel.SupportYears),
            SupportYears = contractRequestModel.SupportYears,
            DiscountId = discount?.DiscountId,
            SoftwareVersion = contractRequestModel.SoftwareVersion,
            IsSigned = contractRequestModel.IsSigned
        };

        await context.Contracts.AddAsync(newContract);
        await context.SaveChangesAsync();

        return GenerateContractResponseModel(newContract);
    }

    public async Task<ContractResponseModel> GetContract(int id)
    {
        var contract = await context.Contracts.FindAsync(id);

        if (contract == null)
        {
            throw new ContractNotFoundException($"Contract with id: {id} has not been found");
        }

        return GenerateContractResponseModel(contract);
    }
    
    public async Task<ContractPaymentResponseModel> PayForContract(ContractPaymentRequestModel contractPaymentRequestModel)
    {
        var contract = await context.Contracts.FindAsync(contractPaymentRequestModel.ContractId);
        if (contract == null)
        {
            throw new ContractNotFoundException(
                $"Contract with id: {contractPaymentRequestModel.ContractId} has not been found");
        }

        if (contract.EndDate.Date < DateTime.Now)
        {
            throw new DateOutOfBoundException(
                $"End date of contract id: ${contractPaymentRequestModel.ContractId} has already passed");
        }
        
        if (contractPaymentRequestModel.PaymentType == PaymentType.Monthly)
        {
            var payment = await context.ContractPayments.Where(c => c.ContractId == contractPaymentRequestModel.ContractId)
                .SumAsync(c => c.Value) + contractPaymentRequestModel.Value;

            if (payment > contract.Price)
            {
                throw new WrongPaymentException("Your payment was too big");
            }
            else if (payment == contract.Price)
            {
                contract.IsSigned = true;
            }
        }
        else
        {
            if (contractPaymentRequestModel.Value != contract.Price)
            {
                throw new WrongPaymentException(
                    $"Your payment: {contractPaymentRequestModel.Value} does not match {contract.Price} given in contract");
            }
            contract.IsSigned = true;
        }

        var contractPayment = new ContractPayment()
        {
            ContractId = contractPaymentRequestModel.ContractId,
            PaymentType = contractPaymentRequestModel.PaymentType,
            Value = contractPaymentRequestModel.Value
        };

        await context.ContractPayments.AddAsync(contractPayment);
        await context.SaveChangesAsync();

        return new ContractPaymentResponseModel()
        {
            ContractPaymentId = contractPayment.PaymentId,
            ClientId = contractPayment.Contract.ClientId,
            ContractId = contractPayment.ContractId,
            PaymentType = contractPayment.PaymentType,
            Value = contractPayment.Value
        };
    }

    private static decimal CalculatePayment(decimal initialPrice, decimal discount, int yearsSupport)
    {
        Console.WriteLine(initialPrice);
        Console.WriteLine(discount);
        Console.WriteLine(yearsSupport);
        var initialPriceWithYearsSupport = initialPrice + (yearsSupport - 1) * 1000;
        return discount == 0 ? initialPriceWithYearsSupport : 
            initialPriceWithYearsSupport - (initialPriceWithYearsSupport * (discount / 100));
    }

    private static ContractResponseModel GenerateContractResponseModel(Contract contract)
    {
        return new ContractResponseModel()
        {
            ContractId = contract.ContractId,
            ClientId = contract.ClientId,
            SoftwareId = contract.SoftwareId,
            SoftwareVersion = contract.SoftwareVersion,
            StartDate = contract.StartDate,
            EndDate = contract.EndDate,
            Price = contract.Price,
            DiscountId = contract.DiscountId,
            SupportYears = contract.SupportYears,
            IsSigned = contract.IsSigned
        };
    }

    
}