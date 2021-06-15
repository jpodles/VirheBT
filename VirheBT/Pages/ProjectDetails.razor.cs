using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Blazorise.DataGrid;

using Microsoft.AspNetCore.Components;

using VirheBT.Infrastructure.Data.Models;
using VirheBT.Services.Interfaces;
using VirheBT.Shared;
using VirheBT.Shared.Enums;
using VirheBT.State;

using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;


namespace VirheBT.Pages
{
    public partial class ProjectDetails
    {
        bool editable = true;
        bool sortable = true;
        bool filterable = true;
        bool showPager = true;
        DataGridEditMode editMode = DataGridEditMode.Popup;
        DataGridSortMode sortMode = DataGridSortMode.Single;
        DataGridSelectionMode selectionMode = DataGridSelectionMode.Single;
        DataGridCommandMode commandsMode = DataGridCommandMode.Default;
        public DataGrid<ApplicationUser> dataGrid;
        public int currentPage { get; set; } = 1;


        public string Name { get; set; }
        public string Description { get; set; }
        public Project CurrentProject { get; set; }
        public DateTime Created { get; set; }
        public ProjectStatus Status { get; set; }

        public List<ApplicationUser> team = new List<ApplicationUser>();
        public List<Issue> issues = new List<Issue>();

        public int ToDoCount { get; set; }
        public int InProgressCount { get; set; }
        public int DoneCount { get; set; }
        public int AllTasks { get; set; }
        public string Maintainer { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Parameter]
        public int ProjectId { get; set; }


        [Inject]
        ProtectedSessionStorage ProtectedSessionStore { get; set; }


        [Inject]
        IProjectService ProjectService { get; set; }

        [Inject]
        IAppState AppState { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Inject]
        IIssueService IssueService { get; set; }

        protected async override Task OnInitializedAsync()
        {

            CurrentProject = await ProjectService.GetProjectAsync(ProjectId);
            issues = await IssueService.GetIssuesAsync(ProjectId);
            Name = CurrentProject.Name;
            Description = CurrentProject.Description;
            FirstName = CurrentProject.Maintainer?.FirstName;
            LastName = CurrentProject.Maintainer?.LastName;
            Status = CurrentProject.Status;
            Created = CurrentProject.Created;
            team = CurrentProject.ApplicationUsers.ToList();


            Maintainer = FirstName + " " + LastName;

            AllTasks = issues.Count();
            ToDoCount = issues
             .Where(x => x.Status == IssueStatus.ToDo).Count();
            InProgressCount = issues
                .Where(x => x.Status == IssueStatus.InProgress).Count();
            DoneCount = issues
                .Where(x => x.Status == IssueStatus.Done).Count();

        }

        protected override void OnAfterRender(bool firstRender)
        {

        }


        private async void SwitchToProject()
        {
            await ProtectedSessionStore.SetAsync("currentProject", ProjectId);
            NavigationManager.NavigateTo("/project/" + ProjectId.ToString() + "/dashboard", true);

        }

        private async void OnDeleteProject()
        {

        }

        //List<UserShortDto> data = new List<UserShortDto>
        //{
        //    new UserShortDto
        //    {
        //        ID = 1,
        //        Name = "Michał Nowak",
        //        Email = "michał@virhe.com",
        //        UserRole = UserRole.Tester
        //    },
        //    new UserShortDto
        //    {
        //        ID = 2,
        //        Name = "Jan Kowalski",
        //        Email = "jan@virhe.com",
        //        UserRole = UserRole.Programmer
        //    },
        //    new UserShortDto
        //    {
        //        ID = 3,
        //        Name = "Tomasz Kowalski",
        //        Email = "tomek@virhe.com",
        //        UserRole = UserRole.ProjectManager
        //    },
        //    new UserShortDto
        //    {
        //        ID = 4,
        //        Name = "Jakub Podleś",
        //        Email = "jakub@virhe.com",
        //        UserRole = UserRole.Programmer
        //    },
        //    new UserShortDto
        //    {
        //        ID = 5,
        //        Name = "Adam Nowak",
        //        Email = "adam@virhe.com",
        //        UserRole = UserRole.Tester
        //    },
        //    new UserShortDto
        //    {
        //        ID = 5,
        //        Name = "Adam Nowak",
        //        Email = "adam@virhe.com",
        //        UserRole = UserRole.Admin
        //    }
        //};
    }
}
