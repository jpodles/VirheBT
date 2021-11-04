using Blazorise;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using VirheBT.Infrastructure.Data.Models;
using VirheBT.Services.Interfaces;
using VirheBT.Shared.Enums;

namespace VirheBT.Pages
{
    public partial class CreateIssue
    {
        [Parameter]
        public int ProjectId { get; set; }

        [Inject]
        private IProjectService ProjectService { get; set; }

        [Inject]
        private IIssueService IssueService { get; set; }

        [Inject]
        private IApplicationUserService ApplicationUserService { get; set; }

        private string SelectedUser { get; set; }
        private Modal createIssueModal;

        private void AssignedUserHandler(string newValue)
        {
            SelectedUser = newValue;
            Console.WriteLine("search value =" + SelectedUser);
        }

        private string Description { get; set; }
        private string Title { get; set; }
        private IssueType CheckedIssueType { get; set; }
        private IssuePriority CheckedIssuePriority { get; set; }

        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        private List<ApplicationUser> projectUsers = new List<ApplicationUser>();

        protected async override Task OnInitializedAsync()
        {
           
        }

        private async Task OnAddIssue()
        {
            var authState = await AuthenticationStateProvider
               .GetAuthenticationStateAsync();
            var user = authState.User.Identity?.Name;

            Issue issueToAdd = new Issue
            {
                ProjectId = ProjectId,
                Title = Title,
                Description = Description,
                Created = DateTime.Now,
                CreatedBy = projectUsers.FirstOrDefault(x => x.Email == user),
                Type = CheckedIssueType,
                Priority = CheckedIssuePriority,
                AssignedTo = projectUsers.FirstOrDefault(x => x.Email == SelectedUser),
            };

            await IssueService.AddIssueAsync(ProjectId, issueToAdd);
            var issues = await IssueService.GetIssuesAsync(ProjectId);
            var issue = issues.OrderByDescending(x => x.Created).First();

            HideModal();
            NavigationManager.NavigateTo($"/project/{ProjectId}/issue/{issue.IssueId}", true);
        }

        public async Task ShowModal()
        {
            projectUsers = await ProjectService.GetProjectUsersAsync(ProjectId);
            createIssueModal.Show();
        }

        private void HideModal()
        {
            projectUsers = new List<ApplicationUser>();
            Title = string.Empty;
            Description = string.Empty;
            CheckedIssueType = default;
            CheckedIssuePriority = default;
      
            StateHasChanged();
            createIssueModal.Hide();
        }
    }
}