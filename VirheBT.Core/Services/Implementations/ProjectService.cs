namespace VirheBT.Services.Implementations;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;

    private readonly IApplicationUserRepository _userRepository;
    private readonly IMapper _mapper;

    public ProjectService(IProjectRepository projectRepository,
        IApplicationUserRepository userRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;  
          _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public async Task AddUserToProjectAsync(string userEmail, int projectId)
    {
        var user = await _userRepository.GetApplicationUserByEmailAsync(userEmail);

        await _projectRepository.AddUserToProjectAsync(user, projectId);
    }

    public async Task CreateProjectAsync(CreateProjectDto createProject)
    {
        var maintainer = await _userRepository.GetApplicationUserByEmailAsync(createProject.MaintainerEmail);

        var project = new Project
        {
            Maintainer = maintainer,
            Name = createProject.Title,
            Description = createProject.Description,
            Status = ProjectStatus.OnTrack,
            Created = DateTime.Now,
        };

        project.ApplicationUsers.Add(maintainer);
        await _projectRepository.CreateProjectAsync(project);
    }

    public async Task<ProjectDto> GetProjectAsync(int projectId)
    {
        var projectEntity = await _projectRepository.GetProjectAsync(projectId);
        return _mapper.Map<ProjectDto>(projectEntity);
    }

    public async Task<List<ProjectDto>> GetProjectsAsync()
    {
        var projectEntities = await _projectRepository.GetProjectsAsync();
        var projects = new List<ProjectDto>();
        foreach (var project in projectEntities)
            projects.Add(_mapper.Map<ProjectDto>(project));
        return projects.ToList();
    }

    public async Task<List<ApplicationUserDto>> GetProjectUsersAsync(int projectId)
    {
        var projectUsers = await _projectRepository.GetProjectUsersAsync(projectId);
        var users = new List<ApplicationUserDto>();
        foreach (var user in projectUsers)
            users.Add(_mapper.Map<ApplicationUserDto>(user));
        return users;
    }

    public async Task RemoveUserFromProject(string userId, int projectId)
    {
        var userEntity = await _userRepository.GetApplicationUserAsync(userId);
        await _projectRepository.RemoveUserFromProjectAsync(userEntity, projectId);
    }

    public async Task DeleteProject(int projectId)
    {
        await _projectRepository.DeleteProject(projectId);
    }

    public async Task UpdateProjectAsync(int projectId, UpdateProjectDto project)
    {
        if (projectId == 0)
            throw new ArgumentException("Parameter cannot has zero value", nameof(projectId));
        if (project == null)
            throw new ArgumentNullException("Object cannot be null", nameof(project));

        await _projectRepository.UpdateProjectAsync(projectId, _mapper.Map<Project>(project));
    }
}