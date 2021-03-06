namespace VirheBT.Infrastructure.Repositories.Interfaces;
public interface IIssueCommentRepository
{
    Task<IEnumerable<IssueComment>> GetIssueCommentsAsync(int issueId);

    Task<IssueComment> GetIssueCommentAsync(int issueId, int commentId);

    Task AddCommentAsync(IssueComment comment);

    Task EditCommentAsync(IssueComment comment);

    Task DeleteCommentAsync(IssueComment issueComment);
}
