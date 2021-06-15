using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VirheBT.Infrastructure.Data.Models;

namespace VirheBT.Infrastructure.Repositories.Interfaces
{
    public interface IIssueCommentRepository
    {
        Task<IEnumerable<IssueComment>> GetIssueCommentsAsync(int projectId, int issueId);

        Task<IssueComment> GetIssueCommentAsync(int projectId, int issueId, int commentId);
        Task AddCommentAsync(int projectId, int issueId, IssueComment comment);
        Task EditCommentAsync(int projectId, int issueId, IssueComment comment);
        Task DeleteCommentAsync(int projectId, int issueId);
    }
}
