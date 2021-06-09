using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VirheBT.Infrastructure.Data.Models;
using VirheBT.Infrastructure.Repositories.Interfaces;

namespace VirheBT.Infrastructure.Repositories.Implementations
{
    public class IssueHistoryRepository : IIssueHistoryRepository
    {
        public async void AddIssueHistoryAsync(int projectId, IssueHistory issueHistory)
        {
            throw new NotImplementedException();
        }
    }
}
