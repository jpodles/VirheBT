namespace VirheBT.Infrastructure.Repositories.Implementations;

public class IssueCommentRepository : IIssueCommentRepository
{
    private ApplicationDbContext context;

    public IssueCommentRepository(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task AddCommentAsync(IssueComment comment)
    {
        context.IssueComments.Add(comment);
        await context.SaveChangesAsync();
    }

    public async Task DeleteCommentAsync(IssueComment comment)
    {
        context.IssueComments.Remove(comment);
        await context.SaveChangesAsync();
    }

    public async Task EditCommentAsync(IssueComment comment)
    {
        context.IssueComments.Update(comment);
        await context.SaveChangesAsync();
    }

    public async Task<IssueComment> GetIssueCommentAsync(int issueId, int commentId)
    {
        return await context.IssueComments.Where(x => x.IssueId == issueId && x.CommentId == commentId).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<IssueComment>> GetIssueCommentsAsync(int issueId)
    {
        return await context.IssueComments.Where(x => x.IssueId == issueId).ToListAsync();
    }
}
