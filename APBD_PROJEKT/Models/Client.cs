using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using APBD_PROJEKT.Helpers.Enums;

namespace APBD_PROJEKT.Models;

[Table("clients")]
public abstract class Client
{
    [Key]
    [Column("id")]
    public int ClientId { get; set; }
    
    [Column("address")]
    public string Address { get; set; }
    
    [Column("email")]
    [EmailAddress]
    public string Email { get; set; }
    
    [Column("phone_number")]
    [Phone]
    public string PhoneNumber { get; set; }
    
    [Column("client_type")]
    public ClientType ClientType { get; set; }

    public IEnumerable<Contract> Contracts { get; set; }
    
}