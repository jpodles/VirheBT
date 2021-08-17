using Blazorise.DataGrid;

using Microsoft.AspNetCore.Components;

using System.Collections.Generic;
using System.Threading.Tasks;

using VirheBT.Infrastructure.Data.Models;
using VirheBT.Services.Interfaces;
using VirheBT.Shared.Enums;

namespace VirheBT.Pages
{
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
        public DataGrid<Issue> dataGrid;
        public int currentPage { get; set; } = 1;

        [Parameter]
        public int ProjectId { get; set; }

        [Inject]
        private IIssueService IssueService { get; set; }

        private List<Issue> issues = new List<Issue>();

        protected async override Task OnInitializedAsync()
        {
            issues = await IssueService.GetIssuesAsync(ProjectId);
        }

        //List<IssueShortDto> data = new List<IssueShortDto>
        //{
        //    new IssueShortDto
        //    {
        //        ID = 1,
        //        Title ="test1",
        //        Status=IssueStatus.Done,
        //        Type = IssueType.Bug,
        //        Priority = IssuePriority.Low,
        //        AssignedTo ="Jan "
        //    },
        //    new IssueShortDto
        //    {
        //        ID = 2,
        //        Title ="test2",
        //        Status=IssueStatus.InProgress,
        //        Type = IssueType.Bug,
        //        Priority = IssuePriority.Normal,
        //        AssignedTo ="Tomasz"
        //    },
        //     new IssueShortDto
        //    {
        //        ID = 387,
        //        Title ="test2",
        //        Status=IssueStatus.ToDo,
        //        Type = IssueType.Bug,
        //        Priority = IssuePriority.High,
        //        AssignedTo ="Michał"
        //    },
        //    new IssueShortDto
        //    {
        //        ID = 467,
        //        Title ="test2",
        //        Status=IssueStatus.InProgress,
        //        Type = IssueType.Feature,
        //        Priority = IssuePriority.Low,
        //        AssignedTo ="Jakub"
        //    },
        //    new IssueShortDto
        //    {
        //        ID = 523,
        //        Title ="test2",
        //        Status=IssueStatus.InProgress,
        //        Type = IssueType.Feature,
        //        Priority = IssuePriority.Normal,
        //        AssignedTo ="Adam"
        //    },
        //     new IssueShortDto
        //    {
        //        ID = 100,
        //        Title ="test1",
        //        Status=IssueStatus.Done,
        //        Type = IssueType.Bug,
        //        Priority = IssuePriority.Low,
        //        AssignedTo ="Adam"
        //    },
        //    new IssueShortDto
        //    {
        //        ID = 200,
        //        Title ="test2",
        //        Status=IssueStatus.InProgress,
        //        Type = IssueType.Bug,
        //        Priority = IssuePriority.Normal,
        //        AssignedTo ="Jakub"
        //    },
        //     new IssueShortDto
        //    {
        //        ID = 300,
        //        Title ="test2",
        //        Status=IssueStatus.InProgress,
        //        Type = IssueType.Feature,
        //        Priority = IssuePriority.Normal,
        //        AssignedTo ="Adam"
        //    },
        //    new IssueShortDto
        //    {
        //        ID = 400,
        //        Title ="test2",
        //        Status=IssueStatus.InProgress,
        //        Type = IssueType.Bug,
        //        Priority = IssuePriority.Normal,
        //        AssignedTo ="Adam"
        //    },
        //    new IssueShortDto
        //    {
        //        ID = 500,
        //        Title ="test2",
        //        Status=IssueStatus.InProgress,
        //        Type = IssueType.Feature,
        //        Priority = IssuePriority.Normal,
        //        AssignedTo ="Kamil"
        //    },
        //      new IssueShortDto
        //    {
        //        ID = 152,
        //        Title ="test1",
        //        Status=IssueStatus.Done,
        //        Type = IssueType.Bug,
        //        Priority = IssuePriority.Low,
        //        AssignedTo ="Kamil"
        //    },
        //    new IssueShortDto
        //    {
        //        ID = 2,
        //        Title ="test2",
        //        Status=IssueStatus.InProgress,
        //        Type = IssueType.Bug,
        //        Priority = IssuePriority.Normal,
        //        AssignedTo ="Tomasz"
        //    },
        //     new IssueShortDto
        //    {
        //        ID = 321,
        //        Title ="test2",
        //        Status=IssueStatus.ToDo,
        //        Type = IssueType.Bug,
        //        Priority = IssuePriority.High,
        //        AssignedTo ="Kamil"
        //    },
        //    new IssueShortDto
        //    {
        //        ID = 413,
        //        Title ="test2",
        //        Status=IssueStatus.InProgress,
        //        Type = IssueType.Feature,
        //        Priority = IssuePriority.Normal,
        //        AssignedTo ="Jakub"
        //    },
        //    new IssueShortDto
        //    {
        //        ID = 5,
        //        Title ="test2",
        //        Status=IssueStatus.InProgress,
        //        Type = IssueType.Feature,
        //        Priority = IssuePriority.Normal,
        //        AssignedTo ="Adam"
        //    },
        //     new IssueShortDto
        //    {
        //        ID = 100,
        //        Title ="test1",
        //        Status=IssueStatus.Done,
        //        Type = IssueType.Bug,
        //        Priority = IssuePriority.Normal,
        //        AssignedTo ="Karol"
        //    },
        //    new IssueShortDto
        //    {
        //        ID = 200,
        //        Title ="test2",
        //        Status=IssueStatus.InProgress,
        //        Type = IssueType.Bug,
        //        Priority = IssuePriority.Normal,
        //        AssignedTo ="Karol"
        //    },
        //     new IssueShortDto
        //    {
        //        ID = 300,
        //        Title ="test2",
        //        Status=IssueStatus.InProgress,
        //        Type = IssueType.Feature,
        //        Priority = IssuePriority.Normal,
        //        AssignedTo ="Karol"
        //    },
        //    new IssueShortDto
        //    {
        //        ID = 400,
        //        Title ="test2",
        //        Status=IssueStatus.InProgress,
        //        Type = IssueType.Bug,
        //        Priority = IssuePriority.Normal,
        //        AssignedTo ="Norbert"
        //    },
        //    new IssueShortDto
        //    {
        //        ID = 500,
        //        Title ="test2",
        //        Status=IssueStatus.InProgress,
        //        Type = IssueType.Feature,
        //        Priority = IssuePriority.Normal,
        //        AssignedTo ="Hubert"
        //    },
        //      new IssueShortDto
        //    {
        //        ID = 1,
        //        Title ="test1",
        //        Status=IssueStatus.Done,
        //        Type = IssueType.Bug,
        //        Priority = IssuePriority.Low,
        //        AssignedTo ="Norbert"
        //    },
        //    new IssueShortDto
        //    {
        //        ID = 2,
        //        Title ="test2",
        //        Status=IssueStatus.InProgress,
        //        Type = IssueType.Bug,
        //        Priority = IssuePriority.Normal,
        //        AssignedTo ="Hubert"
        //    },
        //     new IssueShortDto
        //    {
        //        ID = 3,
        //        Title ="test2",
        //        Status=IssueStatus.ToDo,
        //        Type = IssueType.Bug,
        //        Priority = IssuePriority.High,
        //        AssignedTo ="Norbert"
        //    },
        //    new IssueShortDto
        //    {
        //        ID = 4,
        //        Title ="test2",
        //        Status=IssueStatus.InProgress,
        //        Type = IssueType.Feature,
        //        Priority = IssuePriority.Normal,
        //        AssignedTo ="Hubert"
        //    },
        //    new IssueShortDto
        //    {
        //        ID = 511,
        //        Title ="test2",
        //        Status=IssueStatus.InProgress,
        //        Type = IssueType.Feature,
        //        Priority = IssuePriority.Normal,
        //        AssignedTo ="Jakub"
        //    },
        //     new IssueShortDto
        //    {
        //        ID = 100,
        //        Title ="test1",
        //        Status=IssueStatus.Done,
        //        Type = IssueType.Bug,
        //        Priority = IssuePriority.Normal,
        //        AssignedTo ="Jakub"
        //    },
        //    new IssueShortDto
        //    {
        //        ID = 200,
        //        Title ="test2",
        //        Status=IssueStatus.InProgress,
        //        Type = IssueType.Bug,
        //        Priority = IssuePriority.Normal,
        //        AssignedTo ="Jakub"
        //    },
        //     new IssueShortDto
        //    {
        //        ID = 300,
        //        Title ="test2",
        //        Status=IssueStatus.InProgress,
        //        Type = IssueType.Feature,
        //        Priority = IssuePriority.High,
        //        AssignedTo ="Tomasz"
        //    },
        //    new IssueShortDto
        //    {
        //        ID = 400,
        //        Title ="test2",
        //        Status=IssueStatus.InProgress,
        //        Type = IssueType.Bug,
        //        Priority = IssuePriority.Normal,
        //        AssignedTo ="Kamil"
        //    },
        //    new IssueShortDto
        //    {
        //        ID = 500,
        //        Title ="test2",
        //        Status=IssueStatus.InProgress,
        //        Type = IssueType.Feature,
        //        Priority = IssuePriority.Low,
        //        AssignedTo ="tomeczek"
        //    }
        //};

        private async Task OnRowRemoved(Issue issue)
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
}