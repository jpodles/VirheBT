﻿namespace VirheBT.Services.Interfaces;

public interface IIssueService
{
    Task AddHistoryEntry(ChangeType changeType, int projectId, int issueId, string userId);

    Task<List<IssueHistoryDto>> GetIssueHistory(int issueId);

    Task<List<Issue>> GetIssuesAsync(int projecId);

    Task<Issue> GetIssueAsync(int projectId, int issueId);

    Task AddIssueAsync(int projectId, Issue issue);

    Task EditIssueAsync(int projectId, int issueId, Issue issue);

    Task DeleteIssueAsync(int projectId, int issueId);

    Task<List<IssueCommentDto>> GetIssueCommentsAsync(int projectId, int issueId);

    Task<IssueComment> GetIssueCommentAsync(int issueId, int commentId);

    Task AddCommentAsync(IssueComment comment);

    Task EditCommentAsync(IssueComment comment);

    Task DeleteCommentAsync(int issueId, int commentId);
}
