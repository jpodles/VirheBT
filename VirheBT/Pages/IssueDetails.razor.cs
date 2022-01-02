using Microsoft.Extensions.Logging;

using VirheBT.Shared;

namespace VirheBT.Pages;

public partial class IssueDetails
{
    [CascadingParameter]
    public Error Error { get; set; }

    [Inject] private IMessageService MessageService { get; set; }

    [Parameter]
    public int ProjectId { get; set; }

    [Parameter]
    public int IssueId { get; set; }

    public IssueDto Issue { get; set; }
    public List<IssueCommentDto> IssueComments { get; set; }
    public List<IssueHistoryDto> IssueHistory { get; set; }

    [Inject]
    private IIssueService IssueService { get; set; }

    [Inject]
    private IProjectService ProjectService { get; set; }  
    [Inject] ILogger<IssueDetails> Logger { get; set; }

    [Inject]
    public IApplicationUserService ApplicationUserService { get; set; }

    [Inject]
    private NavigationManager NavigationManager { get; set; }

    [Inject]
    private IHttpContextAccessor httpContextAccessor { get; set; }

    private HttpContext HttpContext { get; set; }

    private string CreatedBy { get; set; }
    private string AssignedTo { get; set; }
    private IssueType IssueType { get; set; }
    private IssueStatus IssueStatus { get; set; }
    private IssuePriority IssuePriority { get; set; }
    private string Title { get; set; }
    private string Description { get; set; }
    private string CommentModalText { get; set; }

    private List<ApplicationUserDto> projectUsers;

    private ApplicationUserDto AssignedUser { get; set; }

    public IssueCommentDto Comment { get; set; }

    private Snackbar successAlert { get; set; }
    private Snackbar failedAlert { get; set; }
    private bool LoadFailed { get; set; }
    protected override async Task OnInitializedAsync()
    {
        HttpContext = httpContextAccessor.HttpContext;
        projectUsers = await ProjectService.GetProjectUsersAsync(ProjectId);
        Issue = new IssueDto();
        IssueComments = new List<IssueCommentDto>();
        IssueHistory = new List<IssueHistoryDto>();
        await GetIssueAsync();
        AssignedUser = Issue.AssignedTo;

        IssueComments = await IssueService.GetIssueCommentsAsync(ProjectId, IssueId);

        IssueHistory = await IssueService.GetIssueHistory(IssueId);
    }

    private void AssignedUserHandler(ApplicationUserDto newValue)
    {
        AssignedUser = newValue;
    }

    private async Task GetIssueAsync()
    {
        LoadFailed = false;
        try
        {
            Issue = await IssueService.GetIssueAsync(ProjectId, IssueId);
        }
        catch (Exception ex)
        {
           LoadFailed= true;
           Error.ProcessError(ex);
        }
        Title = Issue.Title;
        CreatedBy = Issue.CreatedBy?.Email ?? " ";
        AssignedTo = Issue.AssignedTo?.Email ?? " ";
        Description = Issue.Description;
        IssueType = Issue.Type;
        IssueStatus = Issue.Status;
        IssuePriority = Issue.Priority;
    }

    public bool CanChange() => HttpContext.User.IsInRole("Admin") || HttpContext.User.IsInRole("ProjectManager") || HttpContext.User.Identity.Name == CreatedBy;

    private async Task OnEditAsync()
    {
        var issueEdit = new EditIssueDto
        {
            Title = Title,
            Description = Description,
            Type = IssueType,
            CreatedById = Issue.CreatedBy?.Id,
            AssignedToId = AssignedUser?.Id,
            Status = IssueStatus,
            Priority = IssuePriority,
            ModifiedById = HttpContext.User.Identity.Name
        };

        if (Issue.Title == issueEdit.Title
            && Issue.Description == issueEdit.Description
            && Issue.Type == issueEdit.Type
            && Issue.CreatedBy.Id == issueEdit.CreatedById
            && Issue.AssignedTo?.Id == issueEdit.AssignedToId
            && Issue.Status == issueEdit.Status
            && Issue.Priority == issueEdit.Priority)
        {
            return;
        }

        issueEdit.Modified = DateTimeOffset.UtcNow.LocalDateTime;
        try
        {
            await IssueService.EditIssueAsync(ProjectId, IssueId, issueEdit);
        }
        catch (Exception)
        {
            failedAlert.Show();
        }
        successAlert.Show();
        IssueHistory = await IssueService.GetIssueHistory(IssueId);
        StateHasChanged();
    }

    private async Task OnAddCommentAync()
    {
        var comment = new CreateCommentDto
        {
            Created = DateTimeOffset.UtcNow.LocalDateTime,
            IssueId = Issue.IssueId,
            UserId = (await ApplicationUserService.GetApplicationUserByEmailAsync(HttpContext.User.Identity.Name)).Id,
            Text = CommentModalText,
        };

        await IssueService.AddCommentAsync(comment);

        CommentModalText = "";
        AddCommentModal.Hide();
        IssueComments = await IssueService.GetIssueCommentsAsync(ProjectId, IssueId);
        StateHasChanged();
    }

    private async Task OnEditCommentAync()
    {
        var comment = await IssueService.GetIssueCommentAsync(IssueId, Comment.CommentId);
        var edited = new EditCommentDto
        {
            CommentId = comment.CommentId,
            IssueId = comment.Issue.IssueId,
            Text = CommentModalText,
            UserId = comment.User.Email,
        };

        await IssueService.EditCommentAsync(edited);
        CommentModalText = "";
        AddCommentModal.Hide();
        IssueComments = await IssueService.GetIssueCommentsAsync(ProjectId, IssueId);
        StateHasChanged();
        EditCommentModal.Hide();
    }

    private async Task OnDeleteTaskAsync()
    {
        var question = "Are you sure you want to delete this issue?";
        if (!await MessageService.Confirm(question, $"Delete issue #{Issue.IssueId}?",
           x =>
           {
               x.CancelButtonText = "Delete";
               x.ConfirmButtonText = "Cancel";
               x.ShowMessageIcon = false;
           }))
        {
            await IssueService.DeleteIssueAsync(ProjectId, IssueId);
            NavigationManager.NavigateTo($"/project/{ProjectId}/issues");
        }
    }

    private async Task OnDeleteCommentAsync(int commentId)
    {
        var question = "Are you sure you want to delete this comment?";
        if (!await MessageService.Confirm(question, $"Delete comment #{commentId}?",
           x =>
           {
               x.CancelButtonText = "Delete";
               x.ConfirmButtonText = "Cancel";
               x.ShowMessageIcon = false;
           }))
        {
            await IssueService.DeleteCommentAsync(IssueId, commentId);
            IssueComments = await IssueService.GetIssueCommentsAsync(ProjectId, IssueId);
            StateHasChanged();
        }
    }
}