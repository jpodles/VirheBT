namespace VirheBT.Services.Implementations;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly IApplicationUserService _userService;
    private readonly IMapper _mapper;

    public ProjectService(IProjectRepository projectRepository,
        IApplicationUserService userService,
        IMapper mapper)
    {
        _userService = userService;
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public async Task AddUserToProjectAsync(string userEmail, int projectId)
    {
        var user = await _userService.GetApplicationUserByEmailAsync(userEmail);

        await _projectRepository.AddUserToProjectAsync(user, projectId);
    }

    public async Task CreateProjectAsync(string projectName, string description, string maintainerEmail)
    {
        var maintainer = await _userService.GetApplicationUserByEmailAsync(maintainerEmail);

        var project = new Project
        {
            Maintainer = maintainer,
            Name = projectName,
            Description = description,
            Status = ProjectStatus.OnTrack,
            Created = DateTime.Now,
        };

        project.ApplicationUsers.Add(maintainer);
        await _projectRepository.CreateProjectAsync(project);
    }

    public async Task<Project> GetProjectAsync(int projectId)
    {
        return await _projectRepository.GetProjectAsync(projectId);
    }

    public async Task<List<Project>> GetProjectsAsync()
    {
        var projects = await _projectRepository.GetProjectsAsync();

        return projects.ToList();
    }

    public async Task<List<ApplicationUser>> GetProjectUsersAsync(int projectId)
    {
        var projectUsers = await _projectRepository.GetProjectUsersAsync(projectId);
        return projectUsers.ToList();
    }

    public async Task RemoveUserFromProject(string userId, int projectId)
    {
        var userEntity = await _userService.GetApplicationUserAsync(userId);

        await _projectRepository.RemoveUserFromProjectAsync(userEntity, projectId);
    }

    public async Task DeleteProject(Project project)
    {
        await _projectRepository.DeleteProject(project);
    }

    public async Task UpdateProjectAsync(int projectId, Project project)
    {
        if (projectId == 0)
            throw new ArgumentException("Parameter cannot has zero value", nameof(projectId));

        if (project == null)
            throw new ArgumentNullException("Object cannot be null", nameof(project));

        await _projectRepository.UpdateProjectAsync(projectId, project);
    }
}
