using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace webapi.Models;

[Table("item_history")]
public class ItemHistory {
    [Key, Required, Column("history_id")]
    public long HistoryId { get; set; }

    [Required, Column("item_id")]
    public long? ItemId { get; set; }

    [Required, Column("project_id")]
    public long ProjectId { get; set; }

    [Required, Column("owner_id")]
    [ForeignKey("Owner")]
    public long OwnerId { get; set; }

    [Required, Column("parent_id")]
    public long ParentId { get; set; }

    [Required, Column("name")]
    public required string Name { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Required, Column("type")]
    public Type Type { get; set; }

    [Required, Column("created_date")]
    public DateTime CreatedDate { get; set; }

    [Required, Column("state")]
    public State State { get; set; }

    [Required, Column("change")]
    public required string Change { get; set; }

    [Required, Column("change_user_id")]
    [ForeignKey("ChangeUser")]
    public long ChangeUserId { get; set; }

    [JsonIgnore]
    public virtual User? Owner { get; set; }
    [JsonIgnore]
    public virtual User? ChangeUser { get; set; }
}