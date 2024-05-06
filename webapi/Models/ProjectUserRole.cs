using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models;

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

    public virtual Project? Project { get; set; }
    public virtual User? User { get; set; }
    public virtual Role? Role { get; set; }
}