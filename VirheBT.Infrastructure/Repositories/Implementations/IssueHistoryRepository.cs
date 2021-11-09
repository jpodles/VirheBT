namespace VirheBT.Infrastructure.Repositories.Implementations;

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
        return await context.IssueHistories.Include(x => x.User).Where(x => x.IssueId == issueId).ToListAsync();
    }
}
