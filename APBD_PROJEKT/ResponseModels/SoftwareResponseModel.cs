using APBD_PROJEKT.Helpers.Enums;
using APBD_PROJEKT.Models;

namespace APBD_PROJEKT.ResponseModels;

public class SoftwareResponseModel
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public string CurrentVersion { get; set; }
    
    public string SoftwareType { get; set; }
    
}