using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VirheBT.Infrastructure.Data.Models;
using VirheBT.Shared.DTOs;

namespace VirheBT.Services.Interfaces
{
    public interface IProjectService
    {

        Task<List<ProjectShortDto>> GetProjectsAsync();
        Task<Project> GetProjectAsync(int projectId);

        Task<IEnumerable<ApplicationUser>> GetProjectUsersAsync(int projectId);

        void CreateProject(Project project);

        void UpdateProject(int projectId, Project project);

        void RemoveUserFromProject(string userId, int projectId);

    }
}
