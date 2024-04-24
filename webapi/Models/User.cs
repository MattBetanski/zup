using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models;

[Table("user")]
public class User {
    [Key, Required]
    public int user_id { get; set; }

    public string? username { get; set; }    // these should eventually be non-nullable
    public string? first_name { get; set; }  // these should eventually be non-nullable
}
