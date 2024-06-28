using APBD_PROJEKT.Contexts;
using APBD_PROJEKT.Helpers.CurrencyHelpers;
using APBD_PROJEKT.ResponseModels;
using Microsoft.EntityFrameworkCore;

namespace APBD_PROJEKT.Services.IncomeService;

public class IncomeService(DatabaseContext context) : IIncomeService
{
    public async Task<IncomeResponseModel> CalculateCurrentIncome(int? productId, string? currency)
    {
        decimal income = 0;
        if (productId == null)
        {
            income = await context.Contracts.Where(c => c.IsSigned).SumAsync(c => c.Price);
        }
        else
        {
            income = await context.Contracts.Where(c => c.SoftwareId == productId && c.IsSigned).SumAsync(c => c.Price);
        }

        if (currency != null)
        {
            income *= Currency.GetCurrencyRate(currency);
        }

        return new IncomeResponseModel()
        {
            Income = income,
            Currency = currency?.ToUpper() ?? "PLN"
        };
    }

    public async Task<IncomeResponseModel> CalculatePredictedIncome(int? productId, string? currency)
    {
        decimal income = 0;
        if (productId == null)
        {
            income = await context.Contracts.SumAsync(c => c.Price);
        }
        else
        {
            income = await context.Contracts.Where(c => c.SoftwareId == productId).SumAsync(c => c.Price);
        }

        if (currency != null)
        {
            income *= Currency.GetCurrencyRate(currency);
        }

        return new IncomeResponseModel()
        {
            Income = income,
            Currency = currency?.ToUpper() ?? "PLN"
        };
    }
    
}