using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace webapi.Models;

[Table("department")]
public class Department {
    [Key, Required, Column("department_id")]
    public long DepartmentId { get; set; }

    [Required, Column("owner_id")]
    [ForeignKey("User")]
    public long OwnerId { get; set; }

    [Required, Column("name")]
    public required string Name { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Required, Column("creation_date")]
    public DateTime CreationDate { get; set; }

    [Required, Column("visibility")]
    public bool Visibility { get; set; }

    [JsonIgnore]
    public virtual User? Owner { get; set; }
}
