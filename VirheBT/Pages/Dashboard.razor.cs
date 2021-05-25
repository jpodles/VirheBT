using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Blazorise.Charts;

namespace VirheBT.Pages
{
    public partial class Dashboard
    {
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

        private async Task SetDataAndUpdate<TDataSet, TItem, TOptions, TModel>
            (Blazorise.Charts.BaseChart<TDataSet, TItem, TOptions, TModel> chart, Func<List<TItem>> items)
            where TDataSet : ChartDataset<TItem>
            where TOptions : ChartOptions
            where TModel : ChartModel
        {
            await chart.SetData(0, items());
            await chart.Update();
        }

        private BarChartDataset<int> GetBugsBarChartDataset()
        {
            return new BarChartDataset<int>
            {
                Label = "Bugs",
                Data = new List<int> { 5, 7, 9, 0 },
                BackgroundColor = _bugsPriorityColors,
                BorderWidth = 1,
            };
        }

        private BarChartDataset<int> GetFeaturesBarChartDataset()
        {
            return new BarChartDataset<int>
            {
                Label = "Features",
                Data = new List<int> { 5, 7, 9, 0 },
                BackgroundColor = _featuresPriorityColors,
                BorderWidth = 1,
            };
        }

        private PieChartDataset<int> GetFeaturesBugsChartDataset()
        {
            return new PieChartDataset<int>

            {
                Data = new List<int> { 20, 30 },
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
                Data = new List<int> { 10, 20, 30 },
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
                Data = GetData(),
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