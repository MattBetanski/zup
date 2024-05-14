using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace webapi.Models;

[Table("wiki_page")]
public class WikiPage {
    [Key, Required, Column("wiki_page_id")]
    public long WikiPageId { get; set; }

    [Required, Column("department_id")]
    [ForeignKey("Department")]
    public long DepartmentId { get; set; }

    [Required, Column("title")]
    public required string Title { get; set; }

    [Column("content")]
    public string? Content { get; set; }

    [Required, Column("created_date")]
    public DateTime CreatedDate { get; set; }

    [JsonIgnore]
    public virtual Department? Department { get; set; }
}