using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VirheBT.Infrastructure.Data.Models;

namespace VirheBT.Services.Interfaces
{
    public interface IIssueService
    {
        Task<List<Issue>> GetIssuesAsync(int projecId);
        Task<Issue> GetIssueAsync(int projectId, int issueId);

        Task AddIssueAsync(int projectId, Issue issue);
        Task EditIssueAsync(int projectId, int issueId, Issue issue);
        Task DeleteIssueAsync(int projectId, int issueId);


        Task<List<IssueComment>> GetIssueCommentsAsync(int projectId, int issueId);

        Task AddCommentAsync(int projectId, int issueId, IssueComment comment);
        Task EditCommentAsync(int projectId, int issueId, IssueComment comment);
        Task DeleteCommentAsync(int projectId, int issueId, int commentId);
    }
}
