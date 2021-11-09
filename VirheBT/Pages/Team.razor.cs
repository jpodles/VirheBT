namespace VirheBT.Pages;

public class UserShortDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public UserRole UserRole { get; set; }
}

public partial class Team
{
    [Parameter]
    public int ProjectId { get; set; }

    [Inject]
    private IProjectService ProjectService { get; set; }

    [Inject]
    private IApplicationUserService ApplicationUserService { get; set; }

    [Inject]
    private IHttpContextAccessor httpAccessor { get; set; }

    private bool editable = true;
    private bool sortable = true;
    private bool filterable = true;
    private bool showPager = true;
    private DataGridEditMode editMode = DataGridEditMode.Popup;
    private DataGridSortMode sortMode = DataGridSortMode.Single;
    private DataGridSelectionMode selectionMode = DataGridSelectionMode.Single;
    private DataGridCommandMode commandsMode = DataGridCommandMode.Default;
    public DataGrid<ApplicationUser> dataGrid;
    public int currentPage { get; set; } = 1;

    private List<ApplicationUser> data = new List<ApplicationUser>();

    private List<ApplicationUser> appUsers = new List<ApplicationUser>();

    protected override async Task OnInitializedAsync()
    {
        data = await ProjectService.GetProjectUsersAsync(ProjectId);
        appUsers = await ApplicationUserService.GetApplicationUsersAsync();
    }

    private string selectedSearchValue { get; set; }

    private void MySearchHandler(string newValue) => selectedSearchValue = newValue;

    public bool CanAddUser()
    {
        return httpAccessor.HttpContext.User.IsInRole("Admin") || httpAccessor.HttpContext.User.IsInRole("ProjectManager");
    }

    private async Task OnAddUserToProject()
    {
        if (!string.IsNullOrEmpty(selectedSearchValue))
        {
            await ProjectService.AddUserToProjectAsync(selectedSearchValue, ProjectId);
            data = await ProjectService.GetProjectUsersAsync(ProjectId);
            StateHasChanged();
        }
    }

    private async Task OnRowRemoved(ApplicationUser applicationUser)
    {
        await ProjectService.RemoveUserFromProject(applicationUser.Id, ProjectId);
        data = await ProjectService.GetProjectUsersAsync(ProjectId);
        StateHasChanged();
    }
}
