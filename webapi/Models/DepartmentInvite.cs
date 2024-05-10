using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace webapi.Models;

[Table("department_invite")]
public class DepartmentInvite {
    [Required, Column("department_id")]
    [ForeignKey("Department")]
    public long DepartmentId { get; set; }

    [Required, Column("invitee_id")]
    [ForeignKey("User")]
    public long InviteeId { get; set; }

    [Column("response")]
    public int Response { get; set; }

    [JsonIgnore]
    public virtual Department? Department { get; set;}
    [JsonIgnore]
    public virtual User? User { get; set; }


}