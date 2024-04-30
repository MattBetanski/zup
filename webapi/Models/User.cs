using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models;

[Table("user")]
public class User {
    #pragma warning disable CS8618
    [Key, Required]
    public int user_id { get; set; }

    [Required]
    public string username { get; set; }    // these should eventually be non-nullable
    [Required]
    public string hashed_password { get; set; }
    [Required]
    public string email { get; set; }
    public string? first_name { get; set; }  // these should eventually be non-nullable
    #pragma warning restore CS8618
}
