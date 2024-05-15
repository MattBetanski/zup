using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace webapi.Models;

[Table("project")]
public class Project {
    [Key, Required, Column("project_id")]
    public long ProjectId { get; set; }

    [Required, Column("department_id")]
    [ForeignKey("Department")]
    public long DepartmentId { get; set; }
    
    [Column("name")]
    public string Name { get; set; } = string.Empty; // needs to be unique within its department

    [Column("description")]
    public string? Description { get; set; }

    [Column("created_date")]
    public DateTime CreatedDate { get; set; }

    [JsonIgnore]
    public virtual Department? Department { get; set; }
}