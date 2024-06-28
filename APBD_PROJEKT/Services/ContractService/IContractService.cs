using APBD_PROJEKT.Models;
using APBD_PROJEKT.RequestModels;
using APBD_PROJEKT.ResponseModels;

namespace APBD_PROJEKT.Services.ContractService;

public interface IContractService
{
   Task<ContractResponseModel> CreateContract(ContractRequestModel contractRequestModel);

   Task<ContractResponseModel> GetContract(int id);

   Task<ContractPaymentResponseModel> PayForContract(ContractPaymentRequestModel contractPaymentRequestModel);
}