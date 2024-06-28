using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using APBD_PROJEKT.Helpers.Enums;

namespace APBD_PROJEKT.Models;

[Table("contracts")]
public class Contract
{
    [Column("id")]
    [Key]
    public int ContractId { get; set; }
    
    [Column("client_id")]
    [ForeignKey("ClientId")]
    public int ClientId { get; set; }
    public Client Client { get; set; }

    [Column("software_id")]
    [ForeignKey("SoftwareId")]
    public int SoftwareId { get; set; }
    public Software Software { get; set; }

    [Column("start_date")]
    public DateTime StartDate { get; set; }

    [Column("end_date")]
    public DateTime EndDate { get; set; }

    [Column("price")]
    public decimal Price { get; set; }
    
    [Column("support_years")]
    [Range(1,4)]
    public int SupportYears { get; set; }

    [Column("discount_id")]
    [ForeignKey("DiscountId")]
    public int? DiscountId { get; set; }
    public Discount? Discount { get; set; }

    [Column("software_version")]
    public string SoftwareVersion { get; set; }

    [Column("is_signed")]
    public bool IsSigned { get; set; }

    public IEnumerable<ContractPayment> ContractPayments { get; set; }
}