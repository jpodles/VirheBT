using System.Collections.Generic;
using System.Threading.Tasks;

using VirheBT.Infrastructure.Data.Models;

namespace VirheBT.Infrastructure.Repositories.Interfaces
{
    public interface IIssueHistoryRepository
    {
        Task AddIssueHistoryAsync(IssueHistory issueHistory);

        Task<IEnumerable<IssueHistory>> GetIssueHistory(int issueId);
    }
}