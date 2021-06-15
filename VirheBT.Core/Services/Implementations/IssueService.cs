using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using VirheBT.Infrastructure.Data.Models;
using VirheBT.Infrastructure.Repositories.Interfaces;
using VirheBT.Services.Interfaces;

namespace VirheBT.Services.Implementations
{
    public class IssueService : IIssueService
    {
        private readonly IIssueRepository _issueRepo;
        private readonly IIssueCommentRepository _issueCommentRepo;
        private readonly IIssueHistoryRepository _issueHistoryRepo;
        private readonly IProjectService _projectService;

        public IssueService(IIssueRepository issue, IIssueCommentRepository issueComment, IIssueHistoryRepository issueHistory, IProjectService projectService)
        {
            _issueRepo = issue;
            _issueCommentRepo = issueComment;
            _issueHistoryRepo = issueHistory;
            _projectService = projectService;
        }

        public Task AddCommentAsync(int projectId, int issueId, IssueComment comment)
        {
            throw new NotImplementedException();
        }

        public async Task AddIssueAsync(int projectId, Issue issue)
        {
            var issues = await GetIssuesAsync(projectId);
            await _issueRepo.AddIsssueAsync(issues, issue);

        }

        public Task DeleteCommentAsync(int projectId, int issueId, int commentId)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteIssueAsync(int projectId, int issueId)
        {
            var issue = await GetIssueAsync(projectId, issueId);
            await _issueRepo.DeleteIssueAsync(projectId, issue);
        }

        public Task EditCommentAsync(int projectId, int issueId, IssueComment comment)
        {
            throw new NotImplementedException();
        }

        public async Task EditIssueAsync(int projectId, int issueId, Issue issue)
        {


            await _issueRepo.UpdateIssueAsync(projectId, issueId, issue);
        }

        public async Task<Issue> GetIssueAsync(int projectId, int issueId)
        {

            return await _issueRepo.GetIssueByIdAsync(projectId, issueId);

        }

        public Task<List<IssueComment>> GetIssueCommentsAsync(int projectId, int issueId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Issue>> GetIssuesAsync(int projectId)
        {
            var issues = await _issueRepo.GetIssuesAsync(projectId);
            return issues.ToList();
        }
    }
}