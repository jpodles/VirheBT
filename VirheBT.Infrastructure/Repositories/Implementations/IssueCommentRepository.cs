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
        public async void AddCommentAsync(int projectId, int issueId, IssueComment comment)
        {
            throw new NotImplementedException();
        }

        public async void DeleteCommentAsync(int projectId, int issueId)
        {
            throw new NotImplementedException();
        }

        public async void EditCommentAsync(int projectId, int issueId, IssueComment comment)
        {
            throw new NotImplementedException();
        }

        public async Task<IssueComment> GetIssueCommentAsync(int projectId, int issueId, int commentId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<IssueComment>> GetIssueCommentsAsync(int projectId, int issueId)
        {
            throw new NotImplementedException();
        }
    }
}
