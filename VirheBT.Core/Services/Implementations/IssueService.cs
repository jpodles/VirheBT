namespace VirheBT.Services.Implementations;

public class IssueService : IIssueService
{
    private readonly IIssueRepository issueRepo;
    private readonly IIssueCommentRepository issueCommentRepo;
    private readonly IApplicationUserRepository applicationUserRepo;
    private readonly IIssueHistoryRepository issueHistoryRepo;

    private readonly IMapper _mapper;

    public IssueService(IIssueRepository issueRepo,
                        IIssueCommentRepository issueCommentRepo,
                        IApplicationUserRepository applicationUserRepo,
                        IIssueHistoryRepository issueHistoryRepo,
                        IMapper mapper)
    {
        this.issueRepo = issueRepo;
        this.issueCommentRepo = issueCommentRepo;
        this.applicationUserRepo = applicationUserRepo;
        this.issueHistoryRepo = issueHistoryRepo;

        this._mapper = mapper;
    }

    public async Task AddHistoryEntry(ChangeType changeType, int? projectId, int? issueId, string userId)
    {
        if (userId is null)
            throw new ArgumentNullException(nameof(userId));
        if (!projectId.HasValue || projectId == 0)
            throw new ArgumentException(nameof(projectId));
        if (!issueId.HasValue || issueId == 0)
            throw new ArgumentNullException(nameof(issueId));

        var issue = await issueRepo.GetIssueByIdAsync(projectId.Value, issueId.Value);
        var user = await applicationUserRepo.GetApplicationUserByEmailAsync(userId);
        var historyEntry = new IssueHistory
        {
            ChangeType = changeType,
            ChangeDate = issue.Modified,
            Issue = issue,
            User = _mapper.Map<ApplicationUser>(user),
        };

        await issueHistoryRepo.AddIssueHistoryAsync(historyEntry);
    }

    public async Task<List<IssueHistoryDto>> GetIssueHistory(int? issueId)
    {
        if (!issueId.HasValue || issueId == 0)
            throw new ArgumentException(nameof(issueId));

        var issueHistory = await issueHistoryRepo.GetIssueHistory(issueId.Value);

        var historyToReturn = new List<IssueHistoryDto>();
        foreach (var item in issueHistory)
            historyToReturn.Add(_mapper.Map<IssueHistoryDto>(item));

        return historyToReturn;
    }

    public async Task AddCommentAsync(CreateCommentDto comment)
    {
        if (comment == null)
            throw new ArgumentNullException(nameof(comment));
        await issueCommentRepo.AddCommentAsync(_mapper.Map<IssueComment>(comment));
    }

    public async Task AddIssueAsync(int? projectId, CreateIssueDto issue)
    {
        if (!projectId.HasValue || projectId == 0)
            throw new ArgumentException(nameof(projectId));
        if (issue is null)
            throw new ArgumentNullException(nameof(issue));

        var issues = await issueRepo.GetIssuesAsync(projectId.Value);
        var createdBy = await applicationUserRepo.GetApplicationUserAsync(issue.CreatedById);
        var assignedTo = await applicationUserRepo.GetApplicationUserAsync(issue.AssignedToId);

        var issueToAdd = _mapper.Map<Issue>(issue);
        issueToAdd.AssignedToId = null;
        issueToAdd.CreatedById = null;
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

    public async Task DeleteCommentAsync(int? issueId, int? commentId)
    {
        if (!issueId.HasValue || issueId == 0)
            throw new ArgumentException(nameof(issueId));
        if (!commentId.HasValue || commentId == 0)
            throw new ArgumentException(nameof(commentId));

        var comment = await issueCommentRepo.GetIssueCommentAsync(issueId.Value, commentId.Value);
        await issueCommentRepo.DeleteCommentAsync(comment);
    }

    public async Task DeleteIssueAsync(int? projectId, int? issueId)
    {
        if (!issueId.HasValue || issueId == 0)
            throw new ArgumentException(nameof(issueId));
        if (!projectId.HasValue || projectId == 0)
            throw new ArgumentException(nameof(projectId));

        var issue = await issueRepo.GetIssueByIdAsync(projectId.Value, issueId.Value);
        await issueRepo.DeleteIssueAsync(projectId.Value, issue);
    }

    public async Task EditCommentAsync(EditCommentDto comment)
    {
        if (comment == null)
            throw new ArgumentNullException(nameof(comment));
        var commentToEdit = await issueCommentRepo.GetIssueCommentAsync(comment.IssueId, comment.CommentId);
        commentToEdit.Text = comment.Text;
        await issueCommentRepo.EditCommentAsync(commentToEdit);
    }

    public async Task<IssueCommentDto> GetIssueCommentAsync(int? issueId, int? commentId)
    {
        if (!issueId.HasValue || issueId == 0)
            throw new ArgumentException(nameof(issueId));
        if (!commentId.HasValue || commentId == 0)
            throw new ArgumentException(nameof(commentId));

        var issueEntity = await issueCommentRepo.GetIssueCommentAsync(issueId.Value, commentId.Value);
        return _mapper.Map<IssueCommentDto>(issueEntity);
    }

    //Przykład tworzenia łańcucha asynchronicznych zadań
    public async Task EditIssueAsync(int? projectId, int? issueId, 
        EditIssueDto issue)
    {
        var issueToEdit = _mapper.Map<Issue>(issue);

        var edited = issueRepo.UpdateIssueAsync(projectId.Value, issueId.Value, issueToEdit);
        await edited.ContinueWith(async _ => 
            await AddHistoryEntry(ChangeType.Modified, projectId, issueId, issue.ModifiedById));
    }

  


    public async Task<IssueDto> GetIssueAsync(int? projectId, int? issueId)
    {
        if (!issueId.HasValue || issueId == 0)
            throw new ArgumentException(nameof(issueId));
        if (!projectId.HasValue || projectId == 0)
            throw new ArgumentException(nameof(projectId));

        var issueEntity = await issueRepo
            .GetIssueByIdAsync(projectId.Value, issueId.Value);
        return _mapper.Map<IssueDto>(issueEntity);
    }

    public async Task<List<IssueCommentDto>> GetIssueCommentsAsync(int? projectId, int? issueId)
    {
        if (!issueId.HasValue || issueId == 0)
            throw new ArgumentException(nameof(issueId));
        if (!projectId.HasValue || projectId == 0)
            throw new ArgumentException(nameof(projectId));

        var comments = await issueCommentRepo.GetIssueCommentsAsync(issueId.Value);
        var commentsToReturn = new List<IssueCommentDto>();
        foreach (var item in comments)
            commentsToReturn.Add(_mapper.Map<IssueCommentDto>(item));
        return commentsToReturn;
    }

    public async Task<List<IssueDto>> GetIssuesAsync(int? projectId)
    {
        if (!projectId.HasValue || projectId == 0)
            throw new ArgumentException(nameof(projectId));

        var issues = await issueRepo.GetIssuesAsync(projectId.Value);
        var issuesToReturn = new List<IssueDto>();
        foreach (var item in issues)
            issuesToReturn.Add(_mapper.Map<IssueDto>(item));
        return issuesToReturn.ToList();
    }
}