namespace VirheBT.Services.Interfaces;
public interface IProjectService
{
    Task<List<ProjectDto>> GetProjectsAsync();

    Task<ProjectDto> GetProjectAsync(int projectId);

    Task<List<ApplicationUserDto>> GetProjectUsersAsync(int projectId);

    Task CreateProjectAsync(CreateProjectDto createProject);

    Task UpdateProjectAsync(int projectId, UpdateProjectDto project);

    Task RemoveUserFromProject(string userId, int projectId);

    Task AddUserToProjectAsync(string userEmail, int projecId);

    Task DeleteProject(int projectId);
}
