using APBD_PROJEKT.Helpers.Enums;

namespace APBD_PROJEKT.ResponseModels;

public class ContractPaymentResponseModel
{
    public int ContractPaymentId { get; set; }
    public int ClientId { get; set; }
    public int ContractId { get; set; }
    public decimal Value { get; set; }
    public PaymentType PaymentType { get; set; }
}