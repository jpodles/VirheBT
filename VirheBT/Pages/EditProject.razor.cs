using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

using System.Threading.Tasks;

using VirheBT.Infrastructure.Data.Models;
using VirheBT.Services.Interfaces;

namespace VirheBT.Pages
{
    public partial class EditProject
    {
        [Parameter]
        public int ProjectId { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private IProjectService ProjectService { get; set; }

        private string Name { get; set; }
        private string Description { get; set; }

        private Project CurrentProject { get; set; }

        public async void OnEditAsync()
        {
            Project editModel = new Project
            {
                Name = Name,
                Description = Description
            };

            await ProjectService.UpdateProjectAsync(ProjectId, editModel);
            NavigationManager.NavigateTo($"/projects/{ProjectId}");
        }

        protected async override Task OnInitializedAsync()
        {
            await GetData();
        }

        private async Task GetData()
        {
            CurrentProject = await ProjectService.GetProjectAsync(ProjectId);
            Name = CurrentProject.Name;
            Description = CurrentProject.Description;
        }
    }
}