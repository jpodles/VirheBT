using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

using VirheBT.Infrastructure.Data;
using VirheBT.Infrastructure.Data.Models;
using VirheBT.Infrastructure.Repositories.Interfaces;

namespace VirheBT.Infrastructure.Repositories.Implementations
{
    public class IssueRepository : IIssueRepository
    {
        private ApplicationDbContext _context;

        public IssueRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddIsssueAsync(List<Issue> issues, Issue issue)
        {
            _context.Issues.Add(issue);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteIssueAsync(int projectId, Issue issue)
        {
            _context.Issues.Remove(issue);
            await _context.SaveChangesAsync();
        }

        public async Task<Issue> GetIssueByIdAsync(int projectId, int issueId)
        {
            return await _context.Issues
                .Include(x => x.AssignedTo).Include(x => x.CreatedBy)
                .Where(p => p.ProjectId == projectId)
                .Where(i => i.IssueId == issueId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Issue>> GetIssuesAsync(int projectId)
        {
            return await _context.Issues.Include(x => x.AssignedTo)
                .Where(p => p.ProjectId == projectId).ToListAsync();
        }

        public bool IssueExists(int projectId, int issueId)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateIssueAsync(int projectId, int issueId, Issue issue)
        {
            var issueEntity = await GetIssueByIdAsync(projectId, issueId);

            issueEntity.Title = issue.Title;
            issueEntity.Description = issue.Description;
            issueEntity.Priority = issue.Priority;
            issueEntity.Type = issue.Type;
            issueEntity.Status = issue.Status;

            await _context.SaveChangesAsync();
        }
    }
}