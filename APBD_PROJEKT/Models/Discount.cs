using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using APBD_PROJEKT.Helpers.Enums;

namespace APBD_PROJEKT.Models;

[Table("discounts")]
public class Discount
{
    [Column("id")]
    [Key]
    public int DiscountId { get; set; }
    
    [Column("name")]
    public string Name { get; set; }

    [Column("type")]
    public DiscountType Type { get; set; }
    
    [Column("value")]
    [Range(0,100)]
    public decimal Value { get; set; }

    [Column("start_date")]
    public DateTime StartDate { get; set; }
    
    [Column("end_date")]
    public DateTime EndTime { get; set; }

    public IEnumerable<Contract> Contracts { get; set; }

}