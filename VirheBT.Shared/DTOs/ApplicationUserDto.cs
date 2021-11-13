using VirheBT.Shared.Enums;

namespace VirheBT.Shared.DTOs;

public class ApplicationUserDto
{
    public string Id { get; set; }
    public string Email { get; set; }

    public string FirstName { get; set; }
    public string PhoneNumber { get; set; }

    public string LastName { get; set; }

    public UserStatus UserStatus { get; set; }
    public List<ProjectDto> Projects { get; set; } = new List<ProjectDto>();

    public UserRole UserRole { get; set; }

    public List<ProjectDto> ProjectsMaintained { get; set; }
}
