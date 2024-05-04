using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models;

[Table("item_comment")]
public class ItemComment {
    [Required, Column("user_id")]
    [ForeignKey("User")]
    public long UserId { get; set; }

    [Required, Column("item_id")]
    [ForeignKey("Item")]
    public long ItemId { get; set; }

    [Required, Column("comment")]
    public required string Comment { get; set; }

    [Required, Column("comment_date")]
    public DateTime CommentDate { get; set; }
}