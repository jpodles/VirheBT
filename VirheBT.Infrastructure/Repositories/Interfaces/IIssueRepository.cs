using System.Collections.Generic;
using System.Threading.Tasks;

using VirheBT.Infrastructure.Data.Models;

namespace VirheBT.Infrastructure.Repositories.Interfaces
{
    public interface IIssueRepository
    {
        Task<IEnumerable<Issue>> GetIssuesAsync(int projectId);

        Task<Issue> GetIssueByIdAsync(int projectId, int issueId);

        void AddIsssueAsync(int projectId, Issue issue);

        void DeleteIssueAsync(int projectId, Issue issue);

        void UpdateIssueAsync(int projectId, Issue issue);

        bool IssueExists(int projectId, int issueId);

        bool Save();
    }
}