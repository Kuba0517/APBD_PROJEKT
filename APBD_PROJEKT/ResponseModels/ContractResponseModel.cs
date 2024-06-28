namespace APBD_PROJEKT.ResponseModels;

public class ContractResponseModel
{
    public int ContractId { get; set; }
    public int ClientId { get; set; }

    public int SoftwareId { get; set; }
    
    public string SoftwareVersion { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public decimal Price { get; set; }
    public int? DiscountId { get; set; }
    
    public int SupportYears { get; set; }
    
    public bool IsSigned { get; set; }
}