using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

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

        private Issue Issue { get; set; }

        [Inject]
        IIssueService IssueService { get; set; }

        private string CreatedBy { get; set; }
        private string? AssignedTo { get; set; }

        private IssueType IssueType { get; set; }
        private IssueStatus IssueStatus { get; set; }
        private IssuePriority IssuePriority { get; set; }

        private string Title { get; set; }
        private string Descripton { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Issue = await IssueService.GetIssueAsync(ProjectId, IssueId);
            Title = Issue.Title;
            CreatedBy = Issue.CreatedBy.Email;
            AssignedTo = Issue.AssignedTo?.Email ?? " ";
            Descripton = Issue.Description;
            IssueType = Issue.Type;
            IssueStatus = Issue.Status;
            IssuePriority = Issue.Priority;

        }


        private async Task OnEditAsync()
        {
            var issueEdit = new Issue
            {
                Title = Title,
                Description = Descripton,
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
