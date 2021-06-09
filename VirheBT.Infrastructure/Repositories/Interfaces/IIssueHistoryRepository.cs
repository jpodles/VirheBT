using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VirheBT.Infrastructure.Data.Models;

namespace VirheBT.Infrastructure.Repositories.Interfaces
{
    public interface IIssueHistoryRepository
    {
        void AddIssueHistoryAsync(int projectId, IssueHistory issueHistory);
    }
}
