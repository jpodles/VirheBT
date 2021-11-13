using VirheBT.Shared.Enums;

namespace VirheBT.Shared.DTOs
{
    public record class IssueDto
    {
        public int IssueId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public IssueStatus Status { get; set; }
        public IssuePriority Priority { get; set; }
        public IssueType Type { get; set; }

        public ApplicationUserDto CreatedBy { get; set; }
        public string CreatedById { get; set; }

        public ApplicationUserDto AssignedTo { get; set; }
        public string AssignedToId { get; set; }

        public ApplicationUserDto ModifiedBy { get; set; }
        public string ModifiedById { get; set; }

        public ProjectDto Project { get; set; }
        public int ProjectId { get; set; }

        public ICollection<IssueCommentDto> IssueComments { get; set; }
         = new List<IssueCommentDto>();

        public ICollection<IssueHistoryDto> IssueHistory { get; set; }
        = new List<IssueHistoryDto>();
    }
}