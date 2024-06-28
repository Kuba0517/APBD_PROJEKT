using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using APBD_PROJEKT.Helpers.Enums;

namespace APBD_PROJEKT.Models;

public class Company : Client
{
    [Column("company_name")]
    [MaxLength(255)]
    public string CompanyName { get; set; }
    
    
    [Column("krs")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "KRS must contain exactly 10 digits.")]
    public string Krs { get; set; }
    
    public Company()
    {
        ClientType = ClientType.Company;
    }
    
}