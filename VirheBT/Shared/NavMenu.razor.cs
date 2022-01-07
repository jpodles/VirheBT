namespace VirheBT.Shared;

public partial class NavMenu
{
    [Inject]
    private NavigationManager NavigationManager { get; set; }

    private int currentProject;
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        var result = await ProtectedSessionStore.GetAsync<int>("currentProjectId");

        currentProject = result.Value;
        StateHasChanged();
    }
}
