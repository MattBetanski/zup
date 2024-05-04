using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models;

[Table("user")]
public class User {
    [Key, Required, Column("user_id")]
    public long UserId { get; set; }

    [Required, Column("username")]
    public required string Username { get; set; }

    [Required, Column("hashed_password")]
    public required string HashedPassword { get; set; }

    [Required, Column("salt")]
    public required string Salt { get; set; }

    [Required, Column("email")]
    public required string Email { get; set; }

    [Required, Column("first_name")]
    public required string FirstName { get; set; }

    [Required, Column("last_name")]
    public required string LastName { get; set; }

    [Required, Column("join_date")]
    public DateTime JoinDate { get; set; }

    [Required, Column("account_activated")]
    public bool AccountActivated { get; set; }


    // item history collections
    [InverseProperty("Owner")]
    public virtual ICollection<ItemHistory>? OwnedItemHistories { get; set; }

    [InverseProperty("ChangeUser")]
    public virtual ICollection<ItemHistory>? ChangedItemHistories { get; set; }
}
