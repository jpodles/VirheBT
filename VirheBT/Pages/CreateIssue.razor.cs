namespace VirheBT.Pages;

public partial class CreateIssue
{
    [Parameter]
    public int ProjectId { get; set; }

    [Inject]
    private IProjectService ProjectService { get; set; }

    [Inject]
    private IIssueService IssueService { get; set; }

    [Inject]
    private IApplicationUserService ApplicationUserService { get; set; }

    private string SelectedUser { get; set; }
    private Blazorise.Modal createIssueModal;

    private void AssignedUserHandler(string newValue)
    {
        SelectedUser = newValue;
        Console.WriteLine("search value =" + SelectedUser);
    }

    private string Description { get; set; }
    private string Title { get; set; }
    private IssueType CheckedIssueType { get; set; }
    private IssuePriority CheckedIssuePriority { get; set; }

    [Inject]
    private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

    private List<ApplicationUserDto> projectUsers = new();

    private async Task OnAddIssue()
    {
        var authState = await AuthenticationStateProvider
           .GetAuthenticationStateAsync();
        var user = authState.User.Identity?.Name;

        var issueToAdd = new CreateIssueDto
        {
            ProjectId = ProjectId,
            Title = Title,
            Description = Description,
            Created = DateTime.Now,
            CreatedById = projectUsers.Find(x => x.Email == user)?.Id,
            Type = CheckedIssueType,
            Priority = CheckedIssuePriority,
            AssignedToId = projectUsers.Find(x => x.Email == SelectedUser)?.Id,
        };

        await IssueService.AddIssueAsync(ProjectId, issueToAdd);
        var issues = await IssueService.GetIssuesAsync(ProjectId);
        var issue = issues.OrderByDescending(x => x.Created).First();

        HideModal();
        NavigationManager.NavigateTo($"/project/{ProjectId}/issue/{issue.IssueId}", true);
    }

    public async Task ShowModal()
    {
        projectUsers = await ProjectService.GetProjectUsersAsync(ProjectId);
        createIssueModal.Show();
    }

    private void HideModal()
    {
        projectUsers = new List<ApplicationUserDto>();
        Title = string.Empty;
        Description = string.Empty;
        CheckedIssueType = default;
        CheckedIssuePriority = default;

        StateHasChanged();
        createIssueModal.Hide();
    }
}
