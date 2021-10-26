using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

using System.Threading.Tasks;

using VirheBT.Services.Interfaces;

namespace VirheBT.Pages
{
    public partial class CreateProject
    {
        private string title;
        private string description;

        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        private IProjectService ProjectService { get; set; }

        public async Task OnCreateAsync()
        {
            var authState = await AuthenticationStateProvider
                .GetAuthenticationStateAsync();
            var user = authState.User.Identity.Name;

            await ProjectService.CreateProjectAsync(title, description, user);
            title = "";
            description = "";

            NavigationManager.NavigateTo("/projects", true);
        }
    }
}