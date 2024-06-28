using APBD_PROJEKT.Helpers.Enums;

namespace APBD_PROJEKT.RequestModels;

public class RegisterRequestModel
{
    public string Login { get; set; }

    public string Password { get; set; }
    
    public UserType UserType { get; set; }
}