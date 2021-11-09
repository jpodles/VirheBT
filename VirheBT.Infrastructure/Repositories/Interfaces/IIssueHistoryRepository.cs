namespace VirheBT.Infrastructure.Repositories.Interfaces;
public interface IIssueHistoryRepository
{
    Task AddIssueHistoryAsync(IssueHistory issueHistory);

    Task<IEnumerable<IssueHistory>> GetIssueHistory(int issueId);
}
