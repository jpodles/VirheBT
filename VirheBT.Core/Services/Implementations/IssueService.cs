namespace VirheBT.Services.Implementations;

public class IssueService : IIssueService
{
    private readonly IIssueRepository issueRepo;
    private readonly IIssueCommentRepository issueCommentRepo;
    private readonly IApplicationUserRepository applicationUserRepo;
    private readonly IIssueHistoryRepository issueHistoryRepo;
    private readonly IApplicationUserService userService;
    private readonly IMapper _mapper;

    public IssueService(IIssueRepository issueRepo,
                        IIssueCommentRepository issueCommentRepo,
                        IApplicationUserRepository applicationUserRepo,
                        IIssueHistoryRepository issueHistoryRepo,
                        IApplicationUserService userService,
                        IMapper mapper)
    {
        this.issueRepo = issueRepo;
        this.issueCommentRepo = issueCommentRepo;
        this.applicationUserRepo = applicationUserRepo;
        this.issueHistoryRepo = issueHistoryRepo;
        this.userService = userService;
        this._mapper = mapper;
    }

    public async Task AddHistoryEntry(ChangeType changeType, int projectId, int issueId, string userId)
    {
        var issue = await issueRepo.GetIssueByIdAsync(projectId, issueId);
        var user = await userService.GetApplicationUserAsync(userId);
        var historyEntry = new IssueHistory
        {
            ChangeType = changeType,
            ChangeDate = issue.Modified,
            Issue = issue,
            User = _mapper.Map<ApplicationUser>(user),
        };

        await issueHistoryRepo.AddIssueHistoryAsync(historyEntry);
    }

    public async Task<List<IssueHistoryDto>> GetIssueHistory(int issueId)
    {
        var issueHistory = await issueHistoryRepo.GetIssueHistory(issueId);

        var historyToReturn = new List<IssueHistoryDto>();
        foreach (var item in issueHistory)
            historyToReturn.Add(_mapper.Map<IssueHistoryDto>(item));

        return historyToReturn;
    }

    public async Task AddCommentAsync(CreateCommentDto comment)
    {
        await issueCommentRepo.AddCommentAsync(_mapper.Map<IssueComment>(comment));
    }

    public async Task AddIssueAsync(int projectId, CreateIssueDto issue)
    {
        var issues = await issueRepo.GetIssuesAsync(projectId);
        var createdBy = await applicationUserRepo.GetApplicationUserAsync(issue.CreatedById);
        var assignedTo = await applicationUserRepo.GetApplicationUserAsync(issue.AssignedToId);

        var issueToAdd = _mapper.Map<Issue>(issue);
        issueToAdd.CreatedBy = createdBy;
        issueToAdd.AssignedTo = assignedTo;
        await issueRepo.AddIsssueAsync(issues, issueToAdd);

        var historyEntry = new IssueHistory
        {
            ChangeType = ChangeType.Created,
            ChangeDate = issue.Created,
            Issue = issueToAdd,
            User = issueToAdd.CreatedBy
        };
        await issueHistoryRepo.AddIssueHistoryAsync(historyEntry);
    }

    public async Task DeleteCommentAsync(int issueId, int commentId)
    {
        var comment = await issueCommentRepo.GetIssueCommentAsync(issueId, commentId);
        await issueCommentRepo.DeleteCommentAsync(comment);
    }

    public async Task DeleteIssueAsync(int projectId, int issueId)
    {
        var issue = await issueRepo.GetIssueByIdAsync(projectId, issueId);
        await issueRepo.DeleteIssueAsync(projectId, issue);
    }

    public async Task EditCommentAsync(EditCommentDto comment)
    {
        var commentToEdit = await issueCommentRepo.GetIssueCommentAsync(comment.IssueId, comment.CommentId);
        commentToEdit.Text = comment.Text;
        await issueCommentRepo.EditCommentAsync(commentToEdit);
    }

    public async Task<IssueCommentDto> GetIssueCommentAsync(int issueId, int commentId)
    {
        var issueEntity = await issueCommentRepo.GetIssueCommentAsync(issueId, commentId);
        return _mapper.Map<IssueCommentDto>(issueEntity);
    }

    public async Task EditIssueAsync(int projectId, int issueId, EditIssueDto issue)
    {
        var issueToEdit = _mapper.Map<Issue>(issue);
        await issueRepo.UpdateIssueAsync(projectId, issueId, issueToEdit);
        await AddHistoryEntry(ChangeType.Modified, projectId, issueId, issue.ModifiedBy.Id);
    }

    public async Task<IssueDto> GetIssueAsync(int projectId, int issueId)
    {
        var issueEntity = await issueRepo.GetIssueByIdAsync(projectId, issueId);
        return _mapper.Map<IssueDto>(issueEntity);
    }

    public async Task<List<IssueCommentDto>> GetIssueCommentsAsync(int projectId, int issueId)
    {
        var comments = await issueCommentRepo.GetIssueCommentsAsync(issueId);
        var commentsToReturn = new List<IssueCommentDto>();
        foreach (var item in comments)
            commentsToReturn.Add(_mapper.Map<IssueCommentDto>(item));
        return commentsToReturn;
    }

    public async Task<List<IssueDto>> GetIssuesAsync(int projectId)
    {
        var issues = await issueRepo.GetIssuesAsync(projectId);
        var issuesToReturn = new List<IssueDto>();
        foreach (var item in issues)
            issuesToReturn.Add(_mapper.Map<IssueDto>(item));
        return issuesToReturn.ToList();
    }
}