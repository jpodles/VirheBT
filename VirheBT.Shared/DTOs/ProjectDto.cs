using VirheBT.Shared.Enums;

namespace VirheBT.Shared.DTOs;

public record class ProjectDto
{
    public int ProjectId { get; set; }
    public ApplicationUserDto Maintainer { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Created { get; set; }
    public ProjectStatus Status { get; set; }

    public ICollection<ApplicationUserDto> ApplicationUsers = new List<ApplicationUserDto>();

    public IEnumerable<IssueDto> Issues = new List<IssueDto>();
}
