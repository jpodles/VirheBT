using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

using VirheBT.Infrastructure.Data.Models;
using VirheBT.Services.Implementations;
using VirheBT.Services.Interfaces;
using VirheBT.Shared.Enums;

namespace VirheBT.Pages
{
    public partial class CreateIssue
    {
        [Parameter]
        public int ProjectId { get; set; }

        [Inject]
        IProjectService ProjectService { get; set; }


        [Inject]
        IIssueService IssueService { get; set; }

        [Inject]
        IApplicationUserService ApplicationUserService { get; set; }

        //[Inject]
        //ProtectedSessionStorage ProtectedSessionStorage { get; set; }


        //private int CurrentProjectId { get; set; }

        private string SelectedUser { get; set; }
        //private string selectedSearchValue;
        void AssignedUserHandler(string newValue)
        {
            SelectedUser = newValue;
            Console.WriteLine("search value =" + SelectedUser);
        }


        private string Description { get; set; }
        private string Title { get; set; }
        private IssueType CheckedIssueType { get; set; }
        private IssuePriority CheckedIssuePriority { get; set; }


        [Inject]
        AuthenticationStateProvider AuthenticationStateProvider { get; set; }


        List<ApplicationUser> projectUsers = new List<ApplicationUser>();

        Project Project { get; set; }

        protected async override Task OnInitializedAsync()
        {

            projectUsers = await ProjectService.GetProjectUsersAsync(ProjectId);
            //Project = await ProjectService.GetProjectAsync(ProjectId)



        }

        //protected async Task OnAfterRenderAsync(bool firstRender)
        //{

        //    //projectUsers = await ProjectService.GetProjectUsersAsync(CurrentProjectId);
        //}

        async Task OnAddIssue()
        {
            var authState = await AuthenticationStateProvider
               .GetAuthenticationStateAsync();
            var user = authState.User.Identity.Name;

            Issue issueToAdd = new Issue
            {
                ProjectId = ProjectId,
                Title = Title,
                Description = Description,
                Created = DateTime.Now,
                CreatedBy = projectUsers.Where(x => x.Email == user).FirstOrDefault(),
                Type = CheckedIssueType,
                Priority = CheckedIssuePriority,
                AssignedTo = projectUsers.Where(x => x.Email == SelectedUser).FirstOrDefault(),
            };

            await IssueService.AddIssueAsync(ProjectId, issueToAdd);

            NavigationManager.NavigateTo($"/project/{ProjectId}/issues");

            //StateHasChanged();
        }
    }
}
