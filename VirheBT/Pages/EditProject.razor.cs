namespace VirheBT.Pages;

public partial class EditProject
{
    [Parameter]
    public int ProjectId { get; set; }

    [Inject]
    private NavigationManager NavigationManager { get; set; }

    [Inject]
    private IProjectService ProjectService { get; set; }

    [Inject]
    private IApplicationUserService ApplicationUserService { get; set; }

    private string Name { get; set; }
    private string Description { get; set; }

    private Project CurrentProject { get; set; }

    private List<ApplicationUser> appUsers = new List<ApplicationUser>();
    private List<ApplicationUser> allowedUsers = new List<ApplicationUser>();
    private string selectedSearchValue { get; set; } = "XD";

    private Blazorise.Modal editProjectModal;

    private void SearchHandler(string newValue) => selectedSearchValue = newValue;

    private string selectedAutoCompleteText;

    public async void OnEditAsync()
    {
        Project editModel = new Project
        {
            Name = Name,
            Description = Description
        };

        await ProjectService.UpdateProjectAsync(ProjectId, editModel);
        NavigationManager.NavigateTo($"/projects/{ProjectId}", true);
    }

    private async Task GetData()
    {
        appUsers = await ApplicationUserService.GetApplicationUsersAsync();
        CurrentProject = await ProjectService.GetProjectAsync(ProjectId);
        Name = CurrentProject.Name;
        Description = CurrentProject.Description;
    }

    public async Task ShowModal()
    {
        await GetData();
        selectedAutoCompleteText = CurrentProject.Maintainer.Email;
        allowedUsers = appUsers.FindAll(x => x.UserRole is UserRole.ProjectManager or UserRole.Admin);
        editProjectModal.Show();
    }

    private void HideModal()
    {
        editProjectModal.Hide();
    }
}
