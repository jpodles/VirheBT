using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Blazorise.Charts;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

using VirheBT.Infrastructure.Data.Models;
using VirheBT.Services.Implementations;
using VirheBT.Services.Interfaces;
using VirheBT.Shared.Enums;

namespace VirheBT.Pages
{
    public partial class Dashboard
    {
        [Inject]
        ProtectedSessionStorage ProtectedSessionStore { get; set; }

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

        private Random random = new Random(DateTime.Now.Millisecond);
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
        IProjectService ProjectService { get; set; }

        //private string CurrentProjectId { get; set; }

        private string Name { get; set; }
        private string Description { get; set; }
        private Project CurrentProject { get; set; }

        protected async override Task OnInitializedAsync()
        {
            CurrentProject = await ProjectService.GetProjectAsync(ProjectId);
            Name = CurrentProject.Name;
            Description = CurrentProject.Description;
            issues = CurrentProject.Issues.ToList();


            ToDoCount = issues
              .Where(x => x.Status == IssueStatus.ToDo).Count();
            InProgressCount = issues
                .Where(x => x.Status == IssueStatus.InProgress).Count();
            DoneCount = issues
                .Where(x => x.Status == IssueStatus.Done).Count();
            Bugs = issues
              .Where(x => x.Type == IssueType.Bug).Count();
            Features = issues
                .Where(x => x.Type == IssueType.Feature).Count();

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


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {


            if (!isAlreadyInitialised)
            {
                isAlreadyInitialised = true;

                await Task.WhenAll(

                HandleRedraw(bugsPriorityChart, GetBugsBarChartDataset, _priorityLabels),
                HandleRedraw(featuresPriorityChart, GetFeaturesBarChartDataset, _priorityLabels),
                HandleRedraw(allFeatureBugsInProjectChart, GetFeaturesBugsChartDataset, _bugFeatureLabels),
                HandleRedraw(allTasksStatusInProjectChart, GetAllTaskStatusChartDataset, _taskStatusLabels),
                HandleRedraw(yourTasksChart, GetUserTasksChartDataset, _taskStatusLabels));
            }
            this.StateHasChanged();

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

        //private async Task GetProjectId()
        //{
        //    var result = await ProtectedSessionStore.GetAsync<int>("currentProject");
        //    CurrentProjectId = result.Value;
        //    Console.WriteLine("currentProjectId session =" + CurrentProjectId);

        //}

        //private async Task SetDataAndUpdate<TDataSet, TItem, TOptions, TModel>
        //    (Blazorise.Charts.BaseChart<TDataSet, TItem, TOptions, TModel> chart, Func<List<TItem>> items)
        //    where TDataSet : ChartDataset<TItem>
        //    where TOptions : ChartOptions
        //    where TModel : ChartModel
        //{
        //    await chart.SetData(0, items());
        //    await chart.Update();
        //}

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

        private List<int> GetData()
        {
            return new List<int> {
                random.Next(10, 20),
                random.Next(10, 15) ,
                random.Next(10, 30) ,
              };
        }
    }
}