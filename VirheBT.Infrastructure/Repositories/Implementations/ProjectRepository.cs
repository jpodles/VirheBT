using AutoMapper;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using VirheBT.Infrastructure.Data;
using VirheBT.Infrastructure.Data.Models;
using VirheBT.Infrastructure.Repositories.Interfaces;

namespace VirheBT.Infrastructure.Repositories.Implementations
{
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
                .Where(p => p.ProjectId == projectId).FirstOrDefaultAsync();
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

            projectEntity.Name = project.Name;
            projectEntity.Description = project.Description;

            Save();
        }

        public async Task RemoveUserFromProjectAsync(ApplicationUser user, int projectId)
        {
            var project = await GetProjectAsync(projectId);

            if (user != project.Maintainer)
            {
                project.ApplicationUsers.Remove(user);
                Save();
            }
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }

        public async Task DeleteProject(Project project)
        {
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }
    }
}