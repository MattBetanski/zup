using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models;

[Table("item_assignee")]
public class ItemAssignee {
    [Required, Column("item_id")]
    [ForeignKey("Item")]
    public long ItemId { get; set; }

    [Required, Column("assingee_id")]
    [ForeignKey("User")]
    public long AssingeeId { get; set; }

    public virtual Item? Item { get; set; }
    public virtual User? User { get; set; }
}