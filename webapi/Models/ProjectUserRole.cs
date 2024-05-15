using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace webapi.Models;

[Table("project_user_role")]
public class ProjectUserRole {
    [Required, Column("project_id")]
    [ForeignKey("Project")]
    public long ProjectId { get; set; }

    [Required, Column("user_id")]
    [ForeignKey("User")]
    public long UserId { get; set; }

    [Required, Column("role_id")]
    [ForeignKey("Role")]
    public long RoleId { get; set; }

    [JsonIgnore]
    public virtual Project? Project { get; set; }
    [JsonIgnore]
    public virtual User? User { get; set; }
    [JsonIgnore]
    public virtual Role? Role { get; set; }
}