using APBD_PROJEKT.Helpers.Enums;

namespace APBD_PROJEKT.RequestModels;

public class ContractPaymentRequestModel
{
    public int ContractId { get; set; }
    public decimal Value { get; set; }
    public PaymentType PaymentType { get; set; }
}