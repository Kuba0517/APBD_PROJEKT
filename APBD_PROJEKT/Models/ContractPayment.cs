using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using APBD_PROJEKT.Helpers.Enums;

namespace APBD_PROJEKT.Models;

[Table("contracts_payments")]
public class ContractPayment
{
    [Column("id")]
    [Key]
    public int PaymentId { get; set; }
    
    [Column("contract_id")]
    [ForeignKey("ContractId")]
    public int ContractId { get; set; }
    public Contract Contract { get; set; }
    
    [Column("value")] 
    public decimal Value { get; set; }
    
    [Column("payment_type")] 
    public PaymentType PaymentType { get; set; }
}