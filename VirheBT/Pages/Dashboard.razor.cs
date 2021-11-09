namespace VirheBT.Pages;

public partial class Dashboard
{
    [Inject]
    private ProtectedSessionStorage ProtectedSessionStore { get; set; }

    [Parameter]
    public int ProjectId { get; set; }

    private Chart<int> bugsPriorityChart;
    private Chart<int> featuresPriorityChart;
    private Chart<int> allFeatureBugsInProjectChart;
    private Chart<int> allTasksStatusInProjectChart;
    private Chart<int> yourTasksChart;

    private static string _red = ChartColor.FromRgba(255, 0, 0, 0.7f);
    private static string _green = ChartColor.FromRgba(0, 180, 0, 0.7f);

    private string[] _priorityLabels = { "Low", "Normal", "High" };
    private string[] _bugFeatureLabels = { "Bugs", "Features" };
    private string[] _taskStatusLabels = { "To Do", "In progress", "Done" };

    private List<string> _bugsPriorityColors = new List<string> { _red, _red, _red };
    private List<string> _featuresPriorityColors = new List<string> { _green, _green, _green };

    private List<string> _featuresBugsColors = new List<string> { _red, _green };

    private List<string> backgroundColors = new List<string>
                {
                   _red,
                   ChartColor.FromRgba( 54, 162, 235, 0.7f ),
                   _green
                 };

    private bool isAlreadyInitialised;

    // private Random random = new Random(DateTime.Now.Millisecond);
    private List<Issue> issues = new List<Issue>();

    private int ToDoCount { get; set; }
    private int InProgressCount { get; set; }
    private int DoneCount { get; set; }

    private int Bugs { get; set; }
    private int Features { get; set; }
    private int BugLow { get; set; }
    private int BugNormal { get; set; }
    private int BugHigh { get; set; }
    private int FeaturesLow { get; set; }
    private int FeaturesNormal { get; set; }
    private int FeaturesHigh { get; set; }

    [Inject]
    private IProjectService ProjectService { get; set; }

    [Inject]
    private IIssueService IssueService { get; set; }

    //private string CurrentProjectId { get; set; }

    private string Name { get; set; }
    private string Description { get; set; }
    private Project CurrentProject { get; set; }

    protected override async Task OnInitializedAsync()
    {
        CurrentProject = await ProjectService.GetProjectAsync(ProjectId);
        issues = await IssueService.GetIssuesAsync(ProjectId);
        Name = CurrentProject.Name;
        Description = CurrentProject.Description;

        CalculateCount();

        await Task.WhenAll(
             HandleRedraw(bugsPriorityChart, GetBugsBarChartDataset, _priorityLabels),
             HandleRedraw(featuresPriorityChart, GetFeaturesBarChartDataset, _priorityLabels),
             HandleRedraw(allFeatureBugsInProjectChart, GetFeaturesBugsChartDataset, _bugFeatureLabels),
             HandleRedraw(allTasksStatusInProjectChart, GetAllTaskStatusChartDataset, _taskStatusLabels),
             HandleRedraw(yourTasksChart, GetUserTasksChartDataset, _taskStatusLabels));
    }

    private void CalculateCount()
    {
        ToDoCount = issues
         .Count(x => x.Status == IssueStatus.ToDo);
        InProgressCount = issues
            .Count(x => x.Status == IssueStatus.InProgress);
        DoneCount = issues
            .Count(x => x.Status == IssueStatus.Done);
        Bugs = issues
          .Count(x => x.Type == IssueType.Bug);
        Features = issues
            .Count(x => x.Type == IssueType.Feature);

        BugLow = issues.Where(x => x.Type == IssueType.Bug).Count(x => x.Priority == IssuePriority.Low);
        BugNormal = issues
            .Where(x => x.Type == IssueType.Bug).Count(x => x.Priority == IssuePriority.Normal);
        BugHigh = issues
             .Where(x => x.Type == IssueType.Bug).Count(x => x.Priority == IssuePriority.High);

        FeaturesLow = issues.Where(x => x.Type == IssueType.Feature).Count(x => x.Priority == IssuePriority.Low);
        FeaturesNormal = issues
            .Where(x => x.Type == IssueType.Feature).Count(x => x.Priority == IssuePriority.Normal);
        FeaturesHigh = issues
             .Where(x => x.Type == IssueType.Feature).Count(x => x.Priority == IssuePriority.High);
    }

    private async Task HandleRedraw<TDataSet, TItem, TOptions, TModel>
        (Blazorise.Charts.BaseChart<TDataSet, TItem, TOptions, TModel> chart, Func<TDataSet> getDataSet, string[] labels)
        where TDataSet : ChartDataset<TItem>
        where TOptions : ChartOptions
        where TModel : ChartModel
    {
        await chart.Clear();
        await chart.AddLabelsDatasetsAndUpdate(labels, getDataSet());
    }

    private BarChartDataset<int> GetBugsBarChartDataset()
    {
        return new BarChartDataset<int>
        {
            Label = "Bugs",
            Data = new List<int> { BugLow, BugNormal, BugHigh, 0 },
            BackgroundColor = _bugsPriorityColors,
            BorderWidth = 1,
        };
    }

    private BarChartDataset<int> GetFeaturesBarChartDataset()
    {
        return new BarChartDataset<int>
        {
            Label = "Features",
            Data = new List<int> { FeaturesLow, FeaturesNormal, FeaturesHigh, 0 },
            BackgroundColor = _featuresPriorityColors,
            BorderWidth = 1,
        };
    }

    private PieChartDataset<int> GetFeaturesBugsChartDataset()
    {
        return new PieChartDataset<int>
        {
            Data = new List<int> { Bugs, Features },
            BackgroundColor = _featuresBugsColors,
            BorderColor = _featuresBugsColors,
            BorderWidth = 1
        };
    }

    private DoughnutChartDataset<int> GetAllTaskStatusChartDataset()
    {
        return new DoughnutChartDataset<int>
        {
            Label = "# of randoms",
            Data = new List<int> { ToDoCount, InProgressCount, DoneCount },
            BackgroundColor = backgroundColors,
            BorderColor = backgroundColors,
            BorderWidth = 1
        };
    }

    private PolarAreaChartDataset<int> GetUserTasksChartDataset()
    {
        return new PolarAreaChartDataset<int>
        {
            Label = "# of randoms",
            Data = new List<int> { ToDoCount, InProgressCount, DoneCount },
            BackgroundColor = backgroundColors,
            BorderColor = backgroundColors,
            BorderWidth = 1
        };
    }
}