using System.Collections.Generic;
using System.Threading.Tasks;

using VirheBT.Infrastructure.Data.Models;

namespace VirheBT.Infrastructure.Repositories.Interfaces
{
    public interface IIssueRepository
    {
        Task<IEnumerable<Issue>> GetIssuesAsync(int projectId);

        Task<Issue> GetIssueByIdAsync(int projectId, int issueId);

        Task AddIsssueAsync(List<Issue> issues, Issue issue);

        Task DeleteIssueAsync(int projectId, Issue issue);

        Task UpdateIssueAsync(int projectId, int issueId, Issue issue);

        bool IssueExists(int projectId, int issueId);

        bool Save();
    }
}