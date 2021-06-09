using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

using VirheBT.Infrastructure.Data;
using VirheBT.Infrastructure.Data.Models;
using VirheBT.Infrastructure.Repositories.Interfaces;
using VirheBT.Shared.Enums;

namespace VirheBT.Infrastructure.Repositories.Implementations
{
    public class ProjectRepository : IProjectRepository
    {

        private ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProjectRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async void CreateProjectAsync(Project project)
        {
            await _context.Projects.AddAsync(project);
        }

        public async Task<Project> GetProjectAsync(int projectId)
        {
            return await _context.Projects
                .Where(p => p.ProjectId == projectId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Project>> GetProjectsAsync()
        {
            return await _context.Projects
                .OrderBy(p => p.ProjectId).ToListAsync();
        }

        public async Task<IEnumerable<ApplicationUser>> GetProjectUsersAsync(int projectId)
        {
            var project = await GetProjectAsync(projectId);
            return project.ApplicationUsers;

        }

        public async void UpdateProjectAsync(int projectId, Project project)
        {
            var projectEntity = await GetProjectAsync(projectId);
            _mapper.Map(project, projectEntity);

            Save();

        }

        public async void RemoveUserFromProjectAsync(string userId, int projectId)
        {
            var project = await _context.Projects.Include(p => p.ProjectId).FirstAsync();

            var userToRemove = project.ApplicationUsers.Single(u => u.Id == userId);
            project.ApplicationUsers.Remove(userToRemove);
            Save();


        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}