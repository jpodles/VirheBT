using VirheBT.Shared.Enums;

namespace VirheBT.Shared.DTOs;

public record class IssueHistoryDto
{
    public ChangeType ChangeType { get; set; }
    public DateTime ChangeDate { get; set; }
    public ApplicationUserDto User { get; set; }
}
