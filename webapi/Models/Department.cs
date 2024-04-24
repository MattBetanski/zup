using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models;

[Table("department")]
public class Department {
    [Key, Required]
    public int department_id { get; set; }
    public string name { get; set; } = string.Empty;    
}
