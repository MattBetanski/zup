using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models;

[Table("department_member")]
public class DepartmentMember {
    [Required, Column("member_id")]
    [ForeignKey("User")]
    public long MemberId { get; set; }

    [Required, Column("department_id")]
    [ForeignKey("Department")]
    public long DepartmentId { get; set; }

    public virtual User? User { get; set; }
    public virtual Department? Department { get; set; }
}