using Microsoft.AspNetCore.Components;

using System.Threading.Tasks;

namespace VirheBT.Shared
{
    public partial class NavMenu
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private int currentProject;

        protected async override Task OnInitializedAsync()
        {
            // currentUrl = NavigationManager.BaseUri + "/" + AppState.CurrentProjectId;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            var result = await ProtectedSessionStore.GetAsync<int>("currentProject");

            currentProject = result.Value;
            StateHasChanged();
        }
    }
}