using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

using VirheBT.Shared.Components;

namespace VirheBT.Shared
{
    public partial class NavMenu
    {
        [Inject]
        NavigationManager NavigationManager { get; set; }

        private CreateIssue CreateIssueModal { get; set; }

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
