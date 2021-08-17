using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using VirheBT.Infrastructure.Data;
using VirheBT.Infrastructure.Data.Models;
using VirheBT.Infrastructure.Repositories.Interfaces;

namespace VirheBT.Infrastructure.Repositories.Implementations
{
    public class IssueHistoryRepository : IIssueHistoryRepository
    {
        private readonly ApplicationDbContext context;

        public IssueHistoryRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task AddIssueHistoryAsync(IssueHistory issueHistory)
        {
            await context.IssueHistories.AddAsync(issueHistory);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<IssueHistory>> GetIssueHistory(int issueId)
        {
            return await context.IssueHistories.Where(x => x.IssueId == issueId).ToListAsync();
        }
    }
}