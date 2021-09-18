using Blazorise.DataGrid;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;

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


        [Inject]
        IHttpContextAccessor httpContextAccessor { get; set; }
        private HttpContext HttpContext { get; set; }

        private List<Issue> issues = new List<Issue>();

        protected async override Task OnInitializedAsync()
        {
            HttpContext = httpContextAccessor.HttpContext;
            issues = await IssueService.GetIssuesAsync(ProjectId);
        }

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