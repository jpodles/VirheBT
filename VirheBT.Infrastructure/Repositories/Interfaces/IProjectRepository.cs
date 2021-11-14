namespace VirheBT.Infrastructure.Repositories.Interfaces;

public interface IProjectRepository
{
    Task<IEnumerable<Project>> GetProjectsAsync();

    Task<Project> GetProjectAsync(int projectId);

    Task<IEnumerable<ApplicationUser>> GetProjectUsersAsync(int projectId);

    Task CreateProjectAsync(Project project);

    Task UpdateProjectAsync(int projectId, Project project);

    Task RemoveUserFromProjectAsync(ApplicationUser user, int projectId);

    //void DeactivateProjectAsync(int projectId);
    Task AddUserToProjectAsync(ApplicationUser user, int projectId);

    Task DeleteProject(int projectId);
}
