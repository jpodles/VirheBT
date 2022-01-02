using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VirheBT.Infrastructure.Data.Models;
using VirheBT.Infrastructure.Repositories.Interfaces;

namespace VirheBT.Tests
{
    public class MockIssueRepo : IIssueRepository
    {
        public Task AddIsssueAsync(IEnumerable<Issue> issues, Issue issue)
        {
            throw new NotImplementedException();
        }

        public Task DeleteIssueAsync(int projectId, Issue issue)
        {
            throw new NotImplementedException();
        }

        public Task<Issue> GetIssueByIdAsync(int projectId, int issueId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Issue>> GetIssuesAsync(int projectId)
        {
            throw new NotImplementedException();
        }

        public bool IssueExists(int projectId, int issueId)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public Task UpdateIssueAsync(int projectId, int issueId, Issue issue)
        {
            throw new NotImplementedException();
        }
    }

    public class MockProjectRepo : IProjectRepository
    {
        public Task AddUserToProjectAsync(ApplicationUser user, int projectId)
        {
            throw new NotImplementedException();
        }

        public Task CreateProjectAsync(Project project)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProject(int projectId)
        {
            throw new NotImplementedException();
        }

        public Task<Project> GetProjectAsync(int projectId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Project>> GetProjectsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ApplicationUser>> GetProjectUsersAsync(int projectId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveUserFromProjectAsync(ApplicationUser user, int projectId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProjectAsync(int projectId, Project project)
        {
            throw new NotImplementedException();
        }
    }

    public class MockIssueHistoryRepo : IIssueHistoryRepository
    {
        public Task AddIssueHistoryAsync(IssueHistory issueHistory)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IssueHistory>> GetIssueHistory(int issueId)
        {
            throw new NotImplementedException();
        }
    }

    public class MockIssueCommentRepo : IIssueCommentRepository
    {
        public Task AddCommentAsync(IssueComment comment)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCommentAsync(IssueComment issueComment)
        {
            throw new NotImplementedException();
        }

        public Task EditCommentAsync(IssueComment comment)
        {
            throw new NotImplementedException();
        }

        public Task<IssueComment> GetIssueCommentAsync(int issueId, int commentId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IssueComment>> GetIssueCommentsAsync(int issueId)
        {
            throw new NotImplementedException();
        }
    }

    public class MockAppUserRepo : IApplicationUserRepository
    {
        public void DeactivateUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> GetApplicationUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> GetApplicationUserByEmailAsync(string userEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ApplicationUser>> GetApplicationUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserAsync(ApplicationUser applicationUser, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
