namespace VirheBT.Shared.DTOs;

public class EditCommentDto
{
    public int CommentId { get; set; }
    public string Text { get; set; }
    //public DateTime Created { get; set; }
    public int IssueId { get; set; }
    public string UserId { get; set; }
}
