using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using APBD_PROJEKT.Helpers.Enums;

namespace APBD_PROJEKT.Models;

public class IndividualClient : Client
{
    [Column("name")]
    [MaxLength(57)]
    public string Name { get; set; }
    
    [Column("surname")]
    [MaxLength(36)]
    public string Surname { get; set; }
    
    [Column("pesel")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "PESEL must contain exactly 11 digits.")]
    public string Pesel { get; set; }

    [Column("is_deleted")] public bool IsDeleted { get; set; } = false;

    public IndividualClient()
    {
        ClientType = ClientType.Individual;
    }
}