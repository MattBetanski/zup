using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace webapi.Models;

[Table("note_rating")]
public class NoteRating {
    [Required, Column("user_id")]
    [ForeignKey("User")]
    public long UserId { get; set; }

    [Required, Column("note_id")]
    [ForeignKey("Note")]
    public long NoteId { get; set; }

    [Required, Column("rate")]
    public bool Rate { get; set; }  // true if liked

    [JsonIgnore]
    public virtual User? User { get; set; }
    [JsonIgnore]
    public virtual Note? Note { get; set; }
}