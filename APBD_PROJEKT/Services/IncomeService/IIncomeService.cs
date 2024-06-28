using APBD_PROJEKT.ResponseModels;

namespace APBD_PROJEKT.Services.IncomeService;

public interface IIncomeService
{
   Task<IncomeResponseModel> CalculateCurrentIncome(int? productId, string? currency);

   Task<IncomeResponseModel> CalculatePredictedIncome(int? productId, string? currency);
   
}