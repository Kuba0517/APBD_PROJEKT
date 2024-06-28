using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using APBD_PROJEKT.Helpers.Enums;

namespace APBD_PROJEKT.Models;

[Table("users")]
public class User
{
    [Key]
    [Column("id")]
    public int UserId { get; set; }

    [Column("login")]
    public string Login { get; set; }

    [Column("password")]
    public string Password { get; set; }

    [Column("salt")]
    public string Salt { get; set; }
    
    [Column("role")]
    public UserType UserRole { get; set; }
}