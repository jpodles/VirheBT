using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

using VirheBT.Infrastructure.Data.Models;
using VirheBT.Services.Interfaces;
using VirheBT.Shared.Enums;

namespace VirheBT.Shared.Components
{
    public partial class CreateIssue
    {
        [Parameter]
        public int ProjectId { get; set; }

        [Inject]
        IProjectService ProjectService { get; set; }

        [Inject]
        IApplicationUserService ApplicationUserService { get; set; }

        [Inject]
        ProtectedSessionStorage ProtectedSessionStorage { get; set; }


        private int CurrentProjectId { get; set; }

        private string SelectedUser { get; set; }
        private string selectedSearchValue;
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


        protected async override Task OnInitializedAsync()
        {

            projectUsers = await ProjectService.GetProjectUsersAsync(ProjectId);

        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {

            //projectUsers = await ProjectService.GetProjectUsersAsync(CurrentProjectId);
        }

        async Task OnAddIssue()
        {
            var authState = await AuthenticationStateProvider
               .GetAuthenticationStateAsync();
            var user = authState.User.Identity.Name;



            StateHasChanged();
        }
    }
}
