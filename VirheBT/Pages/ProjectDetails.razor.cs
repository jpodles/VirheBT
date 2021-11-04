using Blazorise;
using Blazorise.DataGrid;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using VirheBT.Infrastructure.Data.Models;
using VirheBT.Services.Interfaces;
using VirheBT.Shared.Enums;


namespace VirheBT.Pages
{
    public partial class ProjectDetails
    {
        [Inject] IMessageService MessageService { get; set; }
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

        public string Name { get; set; }
        public string Description { get; set; }
        public Project CurrentProject { get; set; }
        public DateTime Created { get; set; }
        public ProjectStatus Status { get; set; }

        public List<ApplicationUser> Team = new List<ApplicationUser>();
        public List<Issue> Issues = new List<Issue>();

        public int ToDoCount { get; set; }
        public int InProgressCount { get; set; }
        public int DoneCount { get; set; }
        public int AllTasks { get; set; }
        public string Maintainer { get; set; }
        public string MaintainerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Parameter]
        public int ProjectId { get; set; }

        [Inject]
        private ProtectedSessionStorage ProtectedSessionStore { get; set; }

        [Inject]
        private IProjectService ProjectService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private IIssueService IssueService { get; set; }

        [Inject]
        IHttpContextAccessor httpAccessor { get; set; }

        protected async override Task OnInitializedAsync()
        {
            CurrentProject = await ProjectService.GetProjectAsync(ProjectId);
            Issues = await IssueService.GetIssuesAsync(ProjectId);
            Name = CurrentProject.Name;
            Description = CurrentProject.Description;
            FirstName = CurrentProject.Maintainer?.FirstName;
            LastName = CurrentProject.Maintainer?.LastName;
            MaintainerId = CurrentProject.Maintainer?.Id;
            Status = CurrentProject.Status;
            Created = CurrentProject.Created;
            Team = CurrentProject.ApplicationUsers.ToList();

            Maintainer = FirstName + " " + LastName;

            CountTasks();
        }

        private void CountTasks()
        {
            AllTasks = Issues.Count;
            ToDoCount = Issues
             .Count(x => x.Status == IssueStatus.ToDo);
            InProgressCount = Issues
                .Count(x => x.Status == IssueStatus.InProgress);
            DoneCount = Issues
                .Count(x => x.Status == IssueStatus.Done);
        }

        public bool CanChange()
        {
            return httpAccessor.HttpContext.User.IsInRole("Admin") || httpAccessor.HttpContext.User.IsInRole("ProjectManager");
        }

        public bool IsProjectDisabled()
        {
            return CurrentProject?.Status is ProjectStatus.Canceled or ProjectStatus.Finished;
        }
        private async void SwitchToProject()
        {
            await ProtectedSessionStore.SetAsync("currentProject", ProjectId);
            NavigationManager.NavigateTo("/project/" + ProjectId.ToString() + "/dashboard", true);
        }

        private async void OnDeleteProject()
        {
            if (!await MessageService.Confirm("Are you sure you want to delete this project?", $"Delete issue #{CurrentProject.ProjectId}?",
               x =>
               {
                   x.CancelButtonText = "Delete";
                   x.ConfirmButtonText = "Cancel";
                   x.ShowMessageIcon = false;
               }))
            {
                await ProjectService.DeleteProject(CurrentProject);
                NavigationManager.NavigateTo($"/projects");
            }
        }
    }
}