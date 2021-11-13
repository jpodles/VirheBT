namespace VirheBT.Pages;

public partial class Issues
{
    private bool editable = true;
    private bool sortable = true;
    private bool filterable = true;
    private bool showPager = true;
    private DataGridEditMode editMode = DataGridEditMode.Popup;
    private DataGridSortMode sortMode = DataGridSortMode.Single;
    private DataGridSelectionMode selectionMode = DataGridSelectionMode.Single;
    private DataGridCommandMode commandsMode = DataGridCommandMode.Default;
    public DataGrid<IssueDto> dataGrid;
    public int currentPage { get; set; } = 1;

    [Parameter]
    public int ProjectId { get; set; }

    [Inject]
    private IIssueService IssueService { get; set; }

    [Inject]
    private IHttpContextAccessor httpContextAccessor { get; set; }

    private HttpContext HttpContext { get; set; }

    private List<IssueDto> issues = new List<IssueDto>();

    protected override async Task OnInitializedAsync()
    {
        HttpContext = httpContextAccessor.HttpContext;
        issues = await IssueService.GetIssuesAsync(ProjectId);
    }

    private async Task OnRowRemoved(IssueDto issue)
    {
        await IssueService.DeleteIssueAsync(ProjectId, issue.IssueId);
        issues = await IssueService.GetIssuesAsync(ProjectId);
    }
}

public class IssueShortDto
{
    public int ID { get; set; }
    public string Title { get; set; }
    public IssueStatus Status { get; set; }
    public IssuePriority Priority { get; set; }
    public IssueType Type { get; set; }

    public string AssignedTo { get; set; }
}
