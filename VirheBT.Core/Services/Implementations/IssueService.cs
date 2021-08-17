using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using VirheBT.Infrastructure.Data.Models;
using VirheBT.Infrastructure.Repositories.Interfaces;
using VirheBT.Services.Interfaces;
using VirheBT.Shared.Enums;

namespace VirheBT.Services.Implementations
{
    public class IssueService : IIssueService
    {
        private readonly IIssueRepository issueRepo;
        private readonly IIssueCommentRepository issueCommentRepo;
        private readonly IIssueHistoryRepository issueHistoryRepo;
        private readonly IApplicationUserService userService;

        public IssueService(IIssueRepository issueRepo, IIssueCommentRepository issueCommentRepo, IIssueHistoryRepository issueHistoryRepo, IApplicationUserService userService)
        {
            this.issueRepo = issueRepo;
            this.issueCommentRepo = issueCommentRepo;
            this.issueHistoryRepo = issueHistoryRepo;
            this.userService = userService;
        }

        public async Task AddHistoryEntry(ChangeType changeType, int projectId, int issueId, string userId)
        {
            var issue = await issueRepo.GetIssueByIdAsync(projectId, issueId);
            var user = await userService.GetApplicationUserAsync(userId);
            var historyEntry = new IssueHistory
            {
                ChangeType = changeType,
                ChangeDate = DateTime.UtcNow,
                Issue = issue,
                User = user
            };

            await issueHistoryRepo.AddIssueHistoryAsync(historyEntry);
        }

        public async Task<List<IssueHistory>> GetIssueHistory(int issueId)
        {
            var issueHistory = await issueHistoryRepo.GetIssueHistory(issueId);
            return issueHistory.ToList();
        }

        public async Task AddCommentAsync(IssueComment comment)
        {
            await issueCommentRepo.AddCommentAsync(comment);
        }

        public async Task AddIssueAsync(int projectId, Issue issue)
        {
            var issues = await GetIssuesAsync(projectId);
            await issueRepo.AddIsssueAsync(issues, issue);
        }

        public async Task DeleteCommentAsync(int issueId, int commentId)
        {
            var comment = await issueCommentRepo.GetIssueCommentAsync(issueId, commentId);
            await issueCommentRepo.DeleteCommentAsync(comment);
        }

        public async Task DeleteIssueAsync(int projectId, int issueId)
        {
            var issue = await GetIssueAsync(projectId, issueId);
            await issueRepo.DeleteIssueAsync(projectId, issue);
        }

        public async Task EditCommentAsync(IssueComment comment)
        {
            await issueCommentRepo.EditCommentAsync(comment);
        }

        public async Task EditIssueAsync(int projectId, int issueId, Issue issue)
        {
            await issueRepo.UpdateIssueAsync(projectId, issueId, issue);
        }

        public async Task<Issue> GetIssueAsync(int projectId, int issueId)
        {
            return await issueRepo.GetIssueByIdAsync(projectId, issueId);
        }

        public async Task<List<IssueComment>> GetIssueCommentsAsync(int projectId, int issueId)
        {
            var issues = await issueCommentRepo.GetIssueCommentsAsync(issueId);
            return issues.ToList();
        }

        public async Task<List<Issue>> GetIssuesAsync(int projectId)
        {
            var issues = await issueRepo.GetIssuesAsync(projectId);
            return issues.ToList();
        }
    }
}