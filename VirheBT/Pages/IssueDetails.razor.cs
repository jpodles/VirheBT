using Blazorise;
using Blazorise.Snackbar;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VirheBT.Infrastructure.Data.Models;
using VirheBT.Services.Interfaces;
using VirheBT.Shared.DTOs;
using VirheBT.Shared.Enums;

namespace VirheBT.Pages
{
    public partial class IssueDetails
    {
        [Inject] IMessageService MessageService { get; set; }
        [Parameter]
        public int ProjectId { get; set; }

        [Parameter]
        public int IssueId { get; set; }

        public Issue Issue { get; set; }
        public List<IssueCommentDto> IssueComments { get; set; }
        public List<IssueHistoryDto> IssueHistory { get; set; }

        [Inject]
        private IIssueService IssueService { get; set; }

        [Inject]
        private IProjectService ProjectService { get; set; }

        [Inject]
        public IApplicationUserService ApplicationUserService { get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        IHttpContextAccessor httpContextAccessor { get; set; }
        private HttpContext HttpContext { get; set; }

        private string CreatedBy { get; set; }
        private string AssignedTo { get; set; }
        private IssueType IssueType { get; set; }
        private IssueStatus IssueStatus { get; set; }
        private IssuePriority IssuePriority { get; set; }
        private string Title { get; set; }
        private string Description { get; set; }
        private string CommentModalText { get; set; }

        private IEnumerable<ApplicationUser> projectUsers;

        private ApplicationUser AssignedUser { get; set; }

        public IssueCommentDto Comment { get; set; }

        Snackbar successAlert { get; set; }
        Snackbar failedAlert { get; set; }

        protected override async Task OnInitializedAsync()
        {
            HttpContext = httpContextAccessor.HttpContext;
            projectUsers = await ProjectService.GetProjectUsersAsync(ProjectId);
            Issue = new Issue();
            IssueComments = new List<IssueCommentDto>();
            IssueHistory = new List<IssueHistoryDto>();
            await GetIssueAsync();
            AssignedUser = Issue.AssignedTo;

            IssueComments = await IssueService.GetIssueCommentsAsync(ProjectId, IssueId);

            IssueHistory = await IssueService.GetIssueHistory(IssueId);
        }

        private void AssignedUserHandler(ApplicationUser newValue)
        {
            AssignedUser = newValue;
        }

        private async Task GetIssueAsync()
        {
            Issue = await IssueService.GetIssueAsync(ProjectId, IssueId);
            Title = Issue.Title;
            CreatedBy = Issue.CreatedBy?.Email ?? " ";
            AssignedTo = Issue.AssignedTo?.Email ?? " ";
            Description = Issue.Description;
            IssueType = Issue.Type;
            IssueStatus = Issue.Status;
            IssuePriority = Issue.Priority;
        }

        public bool CanChange()
        {
            if(HttpContext.User.IsInRole("Admin") || HttpContext.User.IsInRole("ProjectManager") || HttpContext.User.Identity.Name == CreatedBy)
            {
                return true;
            }
            return false;
        }


        private async Task OnEditAsync()
        {
            var issueEdit = new Issue
            {
                Title = Title,
                Description = Description,
                Type = IssueType,
                CreatedBy = Issue.CreatedBy,
                AssignedTo = AssignedUser,
                Status = IssueStatus,
                Priority = IssuePriority,
                ModifiedBy = await ApplicationUserService.GetApplicationUserByEmailAsync(HttpContext.User.Identity.Name)
            };

            if (Issue.Title == issueEdit.Title
                && Issue.Description == issueEdit.Description
                && Issue.Type == issueEdit.Type
                && Issue.CreatedBy == issueEdit.CreatedBy
                && Issue.AssignedTo == issueEdit.AssignedTo
                && Issue.Status == issueEdit.Status
                && Issue.Priority == issueEdit.Priority)
            {
                return;
            }

            issueEdit.Modified = DateTimeOffset.UtcNow.LocalDateTime;

            try
            {
                await IssueService.EditIssueAsync(ProjectId, IssueId, issueEdit);
            }
            catch (Exception e)
            {
                failedAlert.Show();
            }
            successAlert.Show();
            IssueHistory = await IssueService.GetIssueHistory(IssueId);
            StateHasChanged();
        }

        private async Task OnAddCommentAync()
        {
            var comment = new IssueComment
            {
                Created = DateTimeOffset.UtcNow.LocalDateTime,
                Issue = Issue,
                User = await ApplicationUserService.GetApplicationUserByEmailAsync(HttpContext.User.Identity.Name),
                Text = CommentModalText,
            };


            await IssueService.AddCommentAsync(comment);

            CommentModalText = "";
            AddCommentModal.Hide();
            IssueComments = await IssueService.GetIssueCommentsAsync(ProjectId, IssueId);
            StateHasChanged();
        }

        private async Task OnEditCommentAync()
        {
            var comment = await IssueService.GetIssueCommentAsync(IssueId, Comment.CommentId);
            comment.Text = CommentModalText;

            await IssueService.EditCommentAsync(comment);
            CommentModalText = "";
            AddCommentModal.Hide();
            IssueComments = await IssueService.GetIssueCommentsAsync(ProjectId, IssueId);
            StateHasChanged();
            EditCommentModal.Hide();
        }

        private async Task OnDeleteTaskAsync()
        {
            if (!await MessageService.Confirm("Are you sure you want to delete this issue?", $"Delete issue #{Issue.IssueId}?",
               x =>
               {
                   x.CancelButtonText = "Delete";
                   x.ConfirmButtonText = "Cancel";
                   x.ShowMessageIcon = false;
               }))
            {
                await IssueService.DeleteIssueAsync(ProjectId, IssueId);
                NavigationManager.NavigateTo($"/project/{ProjectId}/issues");
            }
        }

        private async Task OnDeleteCommentAsync(int commentId)
        {
            if (!await MessageService.Confirm("Are you sure you want to delete this comment?", $"Delete comment #{commentId}?",
               x =>
               {
                   x.CancelButtonText = "Delete";
                   x.ConfirmButtonText = "Cancel";
                   x.ShowMessageIcon = false;
               }))
            {

                await IssueService.DeleteCommentAsync(IssueId, commentId);
                IssueComments = await IssueService.GetIssueCommentsAsync(ProjectId, IssueId);
                StateHasChanged();
            }
        }
    }
}
