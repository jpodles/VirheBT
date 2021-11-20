namespace VirheBT.Services.Interfaces;

public interface IIssueService
{
    Task AddHistoryEntry(ChangeType changeType, int? projectId, int? issueId, string userId);

    Task<List<IssueHistoryDto>> GetIssueHistory(int? issueId);

    Task<List<IssueDto>> GetIssuesAsync(int? projecId);

    Task<IssueDto> GetIssueAsync(int? projectId, int? issueId);

    Task AddIssueAsync(int? projectId, CreateIssueDto issue);

    Task EditIssueAsync(int? projectId, int? issueId, EditIssueDto issue);

    Task DeleteIssueAsync(int? projectId, int? issueId);

    Task<List<IssueCommentDto>> GetIssueCommentsAsync(int? projectId, int? issueId);

    Task<IssueCommentDto> GetIssueCommentAsync(int? issueId, int? commentId);

    Task AddCommentAsync(CreateCommentDto comment);

    Task EditCommentAsync(EditCommentDto comment);

    Task DeleteCommentAsync(int? issueId, int? commentId);
}
