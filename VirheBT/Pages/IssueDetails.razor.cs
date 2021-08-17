using Microsoft.AspNetCore.Components;

using System;
using System.Threading.Tasks;

using VirheBT.Infrastructure.Data.Models;
using VirheBT.Services.Interfaces;
using VirheBT.Shared.Enums;

namespace VirheBT.Pages
{
    public partial class IssueDetails
    {
        [Parameter]
        public int ProjectId { get; set; }
        [Parameter]
        public int IssueId { get; set; }
        public Issue Issue { get; set; }

        [Inject]
        private IIssueService IssueService { get; set; }
        private string CreatedBy { get; set; }
        private string? AssignedTo { get; set; }
        private IssueType IssueType { get; set; }
        private IssueStatus IssueStatus { get; set; }
        private IssuePriority IssuePriority { get; set; }

        private string Title { get; set; }
        private string Description { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Issue = await IssueService.GetIssueAsync(ProjectId, IssueId);
            Title = Issue.Title;
            CreatedBy = Issue.CreatedBy.Email;
            AssignedTo = Issue.AssignedTo?.Email ?? " ";
            Description = Issue.Description;
            IssueType = Issue.Type;
            IssueStatus = Issue.Status;
            IssuePriority = Issue.Priority;

        }

        private async Task OnEditAsync()
        {
            var issueEdit = new Issue
            {
                Title = Title,
                Description = Description,
                Type = IssueType,
                CreatedBy = Issue.CreatedBy,
                AssignedTo = Issue.AssignedTo,
                Status = IssueStatus,
                Priority = IssuePriority,
                Modified = DateTime.Now,
            };

            await IssueService.EditIssueAsync(ProjectId, IssueId, issueEdit);
            StateHasChanged();
        }
    }
}