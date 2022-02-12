namespace VirheBT.Infrastructure.Repositories.Implementations;

public class ProjectRepository : IProjectRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ProjectRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task AddUserToProjectAsync(ApplicationUser user, int projectId)
    {
        var project = await GetProjectAsync(projectId);
        project.ApplicationUsers.Add(user);
        Save();
    }

    public async Task CreateProjectAsync(Project project)
    {
        await _context.Projects.AddAsync(project);
        await _context.SaveChangesAsync();
    }

    public async Task<Project> GetProjectAsync(int projectId)
    {
        return await _context.Projects
            .Where(p => p.ProjectId == projectId)
            .Include(x => x.ApplicationUsers)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Project>> GetProjectsAsync()
    {
        return await _context.Projects.Include(p => p.ApplicationUsers)
            .OrderBy(p => p.ProjectId).ToListAsync();
    }

    public async Task<IEnumerable<ApplicationUser>> GetProjectUsersAsync(int projectId)
    {
        return await _context.ApplicationUsers
            .Where(x => x.Projects.Any(p => p.ProjectId == projectId))
            .ToListAsync();
    }

    public async Task UpdateProjectAsync(int projectId, Project project)
    {
        var projectEntity = await GetProjectAsync(projectId);
        var userEntity = _context.ApplicationUsers.Where(x => x.Email == project.Maintainer.Email).FirstOrDefault();
        projectEntity.Name = project.Name;
        projectEntity.Description = project.Description;
        projectEntity.Maintainer = userEntity;

        Save();
    }

    public async Task RemoveUserFromProjectAsync(ApplicationUser user, int projectId)
    {
        var project = await GetProjectAsync(projectId);
        var item = project.ApplicationUsers.SingleOrDefault(x => x.Id == user.Id);
        if (user != project.Maintainer)
        {
            project.ApplicationUsers.Remove(item);

            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
        }
    }

    public bool Save()
    {
        return _context.SaveChanges() >= 0;
    }

    public async Task DeleteProject(int projectId)
    {
        var p = await _context.Projects.SingleOrDefaultAsync(x => x.ProjectId == projectId);
        _context.Projects.Remove(p);
        await _context.SaveChangesAsync();
    }
}
