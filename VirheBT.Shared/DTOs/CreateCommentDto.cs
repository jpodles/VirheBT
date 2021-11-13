namespace VirheBT.Shared.DTOs;

public class CreateCommentDto
{
    public int CommentId { get; set; }
    public string Text { get; set; }
    public DateTime Created { get; set; }
    public IssueDto Issue { get; set; }
    public ApplicationUserDto User { get; set; }
}
