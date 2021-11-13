using VirheBT.Shared.Enums;

namespace VirheBT.Shared.DTOs;

public class CreateIssueDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public IssueStatus Status { get; set; }
    public IssuePriority Priority { get; set; }
    public IssueType Type { get; set; }
    public string CreatedById { get; set; }
    public string AssignedToId { get; set; }
    public int ProjectId { get; set; }
}
