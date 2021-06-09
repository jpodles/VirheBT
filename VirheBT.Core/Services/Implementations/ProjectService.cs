using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using VirheBT.Infrastructure.Data.Models;
using VirheBT.Infrastructure.Repositories.Interfaces;
using VirheBT.Services.Interfaces;
using VirheBT.Shared.DTOs;

namespace VirheBT.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public ProjectService(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public void CreateProject(Project project)
        {
            _projectRepository.CreateProjectAsync(project);
        }

        public async Task<Project> GetProjectAsync(int projectId)
        {
            return await _projectRepository.GetProjectAsync(projectId);
        }

        public async Task<List<ProjectShortDto>> GetProjectsAsync()
        {
            var projects = await _projectRepository.GetProjectsAsync();

            return _mapper.Map<List<ProjectShortDto>>(projects);

        }

        public async Task<IEnumerable<ApplicationUser>> GetProjectUsersAsync(int projectId)
        {
            return await _projectRepository.GetProjectUsersAsync(projectId);
        }

        public void RemoveUserFromProject(string userId, int projectId)
        {
            _projectRepository.RemoveUserFromProjectAsync(userId, projectId);
        }

        public void UpdateProject(int projectId, Project project)
        {
            if (projectId == 0)
            {
                throw new ArgumentException("Parameter cannot has zero value", nameof(projectId));
            }

            if (project == null)
            {
                throw new ArgumentNullException("Object cannot be null", nameof(project));
            }

            _projectRepository.UpdateProjectAsync(projectId, project);
        }
    }
}
