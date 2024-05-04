using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models;

[Table("wiki_page")]
public class WikiPage {
    [Key, Required, Column("wiki_page_id")]
    public long WikiPageId { get; set; }

    [Required, Column("project_id")]
    [ForeignKey("Project")]
    public long ProjectId { get; set; }

    [Required, Column("title")]
    public required string Title { get; set; }

    [Required, Column("file_path")]
    public required string FilePath { get; set; }

    [Required, Column("created_date")]
    public DateTime CreatedDate { get; set; }

    public virtual Project? Project { get; set; }
}