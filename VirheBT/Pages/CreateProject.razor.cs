using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

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

        public async void OnCreateAsync()
        {
            var authState = await AuthenticationStateProvider
                .GetAuthenticationStateAsync();
            var user = authState.User.Identity.Name;

            ProjectService.CreateProject(title, description, user);

            NavigationManager.NavigateTo("/projects");
        }
    }
}