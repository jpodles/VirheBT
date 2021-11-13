namespace VirheBT.Pages;

public partial class Projects
{
    [Inject] private IMessageService MessageService { get; set; }

    private bool editable = true;
    private bool sortable = true;
    private bool filterable = true;
    private bool showPager = true;
    private DataGridEditMode editMode = DataGridEditMode.Popup;
    private DataGridSortMode sortMode = DataGridSortMode.Single;
    private DataGridSelectionMode selectionMode = DataGridSelectionMode.Single;
    private DataGridCommandMode commandsMode = DataGridCommandMode.Default;
    public DataGrid<ProjectDto> dataGrid;
    public List<ProjectDto> data = new List<ProjectDto>();
    public int currentPage { get; set; } = 1;

    [Inject]
    private IProjectService ProjectService { get; set; }

    [Inject]
    private NavigationManager NavigationManager { get; set; }

    [Inject]
    private IHttpContextAccessor httpContextAccessor { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (httpContextAccessor.HttpContext.User.IsInRole("Admmin"))
        {
            data = await ProjectService.GetProjectsAsync();
        }
        else
        {
            var projects = await ProjectService.GetProjectsAsync();
            data = projects
                    .Where(x => x.ApplicationUsers
                    .Any(x => x.Email == httpContextAccessor.HttpContext.User.Identity.Name))
                    .ToList();
        }
    }

    private async Task OnRowRemoved(ProjectDto project)
    {
        if (!await MessageService.Confirm("Are you sure you want to delete this project?", $"Delete {project.Name}?",
            x =>
            {
                x.CancelButtonText = "Delete";
                x.ConfirmButtonText = "Cancel";
                x.ShowMessageIcon = false;
            }))
        {
            await ProjectService.DeleteProject(project);
            NavigationManager.NavigateTo("/projects", true);
        }
    }
}
