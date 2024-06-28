using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using APBD_PROJEKT.Helpers.Enums;

namespace APBD_PROJEKT.Models;

[Table("softwares")]
public class Software
{
    [Column("id")]
    [Key]
    public int SoftwareId { get; set; }
    
    [Column("name")]
    public string Name { get; set; }

    [Column("description")]
    public string Description { get; set; }

    [Column("current_version")]
    public string CurrentVersion { get; set; }
    
    [Column("software_type")]
    public SoftwareType SoftwareType { get; set; }

    public IEnumerable<Contract> Contracts { get; set; }
}