using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async void AddIsssueAsync(int projectId, Issue issue)
        {
            throw new NotImplementedException();
        }

        public async void DeleteIssueAsync(int projectId, Issue issue)
        {
            throw new NotImplementedException();
        }

        public async Task<Issue> GetIssueByIdAsync(int projectId, int issueId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Issue>> GetIssuesAsync(int projectId)
        {
            throw new NotImplementedException();
        }

        public bool IssueExists(int projectId, int issueId)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public async void UpdateIssueAsync(int projectId, Issue issue)
        {
            throw new NotImplementedException();
        }
    }
}