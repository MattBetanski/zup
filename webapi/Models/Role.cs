using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models;

[Table("role")]
public class Role {
    [Key, Required, Column("role_id")]
    public int RoleId { get; set; }

    [Required, Column("department_id")]
    [ForeignKey("Department")]
    public int DepartmentId { get; set; }

    [Required, Column("name")]
    public required string Name { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Required, Column("read_level")]
    public int ReadLevel { get; set; }

    [Required, Column("write_level")]
    public int WriteLevel { get; set; }

    [Required, Column("create_level")]
    public int CreateLevel { get; set; }

    [Required, Column("delete_level")]
    public int DeleteLevel { get; set; }

    [Required, Column("wiki_level")]
    public int WikiLevel { get; set; }

    [Required, Column("wiki_delete")]
    public bool WikiDelete { get; set; }

    public virtual Department? Department { get; set; }
}