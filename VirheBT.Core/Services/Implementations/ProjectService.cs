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

    public async Task AddUserToProjectAsync(string userEmail, int? projectId)
    {
        if (!projectId.HasValue || projectId == 0)
            throw new ArgumentException(nameof(projectId));
        if(userEmail == null)
            throw new ArgumentNullException(nameof(userEmail));

        var user = await _userRepository.GetApplicationUserByEmailAsync(userEmail);

        await _projectRepository.AddUserToProjectAsync(user, projectId.Value);
    }

    public async Task CreateProjectAsync(CreateProjectDto createProject)
    {
        if (createProject == null)
            throw new ArgumentNullException(nameof(createProject));

        var maintainer = await _userRepository
            .GetApplicationUserByEmailAsync(createProject.MaintainerEmail);

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

    public async Task<ProjectDto> GetProjectAsync(int? projectId)
    {
        if (!projectId.HasValue || projectId == 0)
            throw new ArgumentException(nameof(projectId));
        var projectEntity = await _projectRepository.GetProjectAsync(projectId.Value);
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

    public async Task<List<ApplicationUserDto>> GetProjectUsersAsync(int? projectId)
    {
        if (!projectId.HasValue || projectId == 0)
            throw new ArgumentException(nameof(projectId));
        var projectUsers = await _projectRepository.GetProjectUsersAsync(projectId.Value);
        var users = new List<ApplicationUserDto>();
        foreach (var user in projectUsers)
            users.Add(_mapper.Map<ApplicationUserDto>(user));
        return users;
    }

    public async Task RemoveUserFromProject(string userId, int? projectId)
    {
        if(userId == null)
            throw new ArgumentNullException(nameof(userId));
        if (!projectId.HasValue || projectId == 0)
            throw new ArgumentException(nameof(projectId));
        var userEntity = await _userRepository.GetApplicationUserAsync(userId);
        await _projectRepository.RemoveUserFromProjectAsync(userEntity, projectId.Value);
    }

    public async Task DeleteProject(int? projectId)
    {
        if (!projectId.HasValue || projectId == 0)
            throw new ArgumentException(nameof(projectId));
        await _projectRepository.DeleteProject(projectId.Value);
    }

    public async Task UpdateProjectAsync(int? projectId, UpdateProjectDto project)
    {
        if (!projectId.HasValue || projectId == 0)
            throw new ArgumentException(nameof(projectId));
        if(project is null)
            throw new ArgumentNullException(nameof(project));

        await _projectRepository.UpdateProjectAsync(projectId.Value, _mapper.Map<Project>(project));
    }
}