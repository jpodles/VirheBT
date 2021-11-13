namespace VirheBT.Infrastructure.Data.Models;
public class IssueComment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CommentId { get; set; }
    public string Text { get; set; }
    public DateTime Created { get; set; }
    public int IssueId { get; set; }
    public Issue Issue { get; set; }
    public ApplicationUser User { get; set; }
    public string UserId { get; set; }
}