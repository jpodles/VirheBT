using System;
using System.Collections.Generic;

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

        public IssueService(IIssueRepository issue, IIssueCommentRepository issueComment, IIssueHistoryRepository issueHistory)
        {
            _issueRepo = issue;
            _issueCommentRepo = issueComment;
            _issueHistoryRepo = issueHistory;
        }

        public IEnumerable<Issue> GetIssues(int projecId)
        {
            throw new NotImplementedException();
        }

        public Issue GetIssue(int projectId, int issueId)
        {
            throw new NotImplementedException();
        }

        public void AddIssue(int ProjectId, Issue issue)
        {
            //var project = projectrepo.getproject(int projecid); <-- to w reposisotry
            //issuerepo.addissue(project, issue);
            //isshuehistoryrepo.addissuehistory(changetype.created);
        }

        public void EditIssue(int projectId, int issueId, Issue issue)
        {
            throw new NotImplementedException();
        }

        public void DeleteIssue(int projectId, int issueId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IssueComment> GetIssueComments(int projectId, int issueId)
        {
            throw new NotImplementedException();
        }

        public void DeleteComment(int projectId, int issueId, int commentId)
        {
            throw new NotImplementedException();
        }

        public void AddComment(int projectId, int issueId, IssueComment comment)
        {
            throw new NotImplementedException();
        }

        public void EditComment(int projectId, int issueId, IssueComment comment)
        {
            throw new NotImplementedException();
        }
    }
}