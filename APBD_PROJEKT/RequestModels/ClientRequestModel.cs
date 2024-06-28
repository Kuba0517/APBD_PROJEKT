using APBD_PROJEKT.Helpers.Enums;

namespace APBD_PROJEKT.RequestModels;

public class ClientRequestModel
{
    public string Address { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }
    
    public string CompanyName { get; set; }

    public string Krs { get; set; }
    
    public string Name { get; set; }
    
    public string Surname { get; set; }
    
    public string Pesel { get; set; }
}