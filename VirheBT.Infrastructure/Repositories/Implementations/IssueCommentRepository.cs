using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VirheBT.Infrastructure.Data.Models;
using VirheBT.Infrastructure.Repositories.Interfaces;

namespace VirheBT.Infrastructure.Repositories.Implementations
{
    public class IssueCommentRepository : IIssueCommentRepository
    {
        public Task AddCommentAsync(int projectId, int issueId, IssueComment comment)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCommentAsync(int projectId, int issueId)
        {
            throw new NotImplementedException();
        }

        public Task EditCommentAsync(int projectId, int issueId, IssueComment comment)
        {
            throw new NotImplementedException();
        }

        public Task<IssueComment> GetIssueCommentAsync(int projectId, int issueId, int commentId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IssueComment>> GetIssueCommentsAsync(int projectId, int issueId)
        {
            throw new NotImplementedException();
        }
    }
}
