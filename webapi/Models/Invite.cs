using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace webapi.Models;

[Table("invite")]
public class Invite {
    [Required, Column("department_id")]
    public long DepartmentId { get; set; }

    [Required, Column("invitee_id")]
    public long InviteeId { get; set; }

    [JsonIgnore]
    public virtual Department? Department { get; set; }
    [JsonIgnore]
    public virtual User? Invitee { get; set; }
}