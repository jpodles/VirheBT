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

        Task<List<Project>> GetProjectsAsync();
        Task<Project> GetProjectAsync(int projectId);

        Task<List<ApplicationUser>> GetProjectUsersAsync(int projectId);

        void CreateProject(string title, string description, string maintainerEmail);

        Task UpdateProjectAsync(int projectId, Project project);

        Task RemoveUserFromProject(string userId, int projectId);

        Task AddUserToProjectAsync(string userEmail, int projecId);

    }
}
