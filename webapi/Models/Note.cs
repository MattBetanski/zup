using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models;

[Table("note")]
public class Note {
    [Key, Required, Column("note_id")]
    public long NoteId { get; set; }

    [Required, Column("owner_id")]
    [ForeignKey("User")]
    public long OwnerId { get; set; }

    [Required, Column("title")]
    public required string Title { get; set; }

    [Required, Column("content")]
    public required string Content { get; set; }

    [Required, Column("created_date")]
    public DateTime CreatedDate { get; set; }

    [Required, Column("like_count")]
    public int LikeCount { get; set; }

    [Required, Column("department_id")]
    [ForeignKey("Department")]
    public long DepartmentId { get; set; }

    [Required, Column("project_id")]
    [ForeignKey("Project")]
    public long ProjectId { get; set; }

    public virtual User? Owner { get; set; }
    public virtual Project? Project { get; set; }
    public virtual Department? Department { get; set; }
}