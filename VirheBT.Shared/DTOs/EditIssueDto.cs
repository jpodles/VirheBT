using VirheBT.Shared.Enums;

namespace VirheBT.Shared.DTOs;

public class EditIssueDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public IssueStatus Status { get; set; }
    public IssuePriority Priority { get; set; }
    public IssueType Type { get; set; }
    public ApplicationUserDto CreatedBy { get; set; }
    public ApplicationUserDto AssignedTo { get; set; }
    public ApplicationUserDto ModifiedBy { get; set; }
}
