namespace VirheBT.Infrastructure.Repositories.Implementations;

public class IssueRepository : IIssueRepository
{
    private ApplicationDbContext _context;

    public IssueRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddIsssueAsync(IEnumerable<Issue> issues, Issue issue)
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
        var assignedTo = _context.ApplicationUsers.FirstOrDefault(x => x.Id == issue.AssignedToId);
        issueEntity.Title = issue.Title;
        issueEntity.Description = issue.Description;
        issueEntity.Priority = issue.Priority;
        issueEntity.AssignedTo = assignedTo;
        issueEntity.Type = issue.Type;
        issueEntity.Modified = issue.Modified;
        issueEntity.Status = issue.Status;

        await _context.SaveChangesAsync();
    }
}
