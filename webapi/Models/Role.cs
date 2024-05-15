using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace webapi.Models;

[Table("role")]
public class Role {
    [Key, Required, Column("role_id")]
    public long RoleId { get; set; }

    [Required, Column("department_id")]
    [ForeignKey("Department")]
    public long DepartmentId { get; set; }

    [Required, Column("name")]
    public required string Name { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Required, Column("item_level")]
    public RoleLevel ItemLevel { get; set;}

    [Required, Column("wiki_level")]
    public RoleLevel WikiLevel { get; set; }

    [Required, Column("wiki_delete")]
    public bool WikiDelete { get; set; }

    [JsonIgnore]
    public virtual Department? Department { get; set; }
}