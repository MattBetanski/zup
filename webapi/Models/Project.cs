using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models;

[Table("project")]
public class Project {
    [Key, Required]
    public int project_id { get; set; }
    public string name { get; set; } = string.Empty;
    public virtual Department? department { get; set; }
}