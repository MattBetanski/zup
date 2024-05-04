using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models;

[Table("item")]
public class Item {
    [Key, Required, Column("item_id")]
    public long ItemId { get; set; }

    [Required, Column("project_id")]
    [ForeignKey("Project")]
    public long ProjectId { get; set; }

    [Required, Column("owner_id")]
    [ForeignKey("User")]
    public long OwnerId { get; set; }

    [Column("parent_id")]
    [ForeignKey("Item")]
    public long? ParentId { get; set; }

    [Required, Column("Name")]
    public required string Name { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Required, Column("type")]
    public Type Type { get; set; }

    [Required, Column("confidentiality")]
    public bool Confidentiality { get; set; }

    [Required, Column("created_date")]
    public DateTime CreatedDate { get; set; }

    [Column("deadline")]
    public DateTime? Deadline { get; set; }

    [Required, Column("state")]
    public State State { get; set; }

    public virtual Project? Project { get; set; }
    public virtual User? Owner { get; set; }
    public virtual Item? Parent { get; set; }
}