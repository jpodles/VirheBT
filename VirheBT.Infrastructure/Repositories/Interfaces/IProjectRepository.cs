using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VirheBT.Infrastructure.Data.Models;

namespace VirheBT.Infrastructure.Repositories.Interfaces
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetProjectsAsync();
        Task<Project> GetProjectAsync(int projectId);

        Task<IEnumerable<ApplicationUser>> GetProjectUsersAsync(int projectId);

        void CreateProjectAsync(Project project);

        void UpdateProjectAsync(int projectId, Project project);

        void RemoveUserFromProjectAsync(string userId, int projectId);

        //void DeactivateProjectAsync(int projectId);

    }
}
