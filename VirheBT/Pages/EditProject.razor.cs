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

    private ProjectDto CurrentProject { get; set; }

    private List<ApplicationUserDto> appUsers = new List<ApplicationUserDto>();
    private List<ApplicationUserDto> allowedUsers = new List<ApplicationUserDto>();
    private string selectedSearchValue { get; set; }

    private Blazorise.Modal editProjectModal;

    private void SearchHandler(string newValue) => selectedSearchValue = newValue;

    private string selectedAutoCompleteText;

    public async void OnEditAsync()
    {
        var user = selectedSearchValue;
        if (selectedSearchValue == null)
            user = selectedAutoCompleteText;

        var editModel = new UpdateProjectDto
        {
            Name = Name,
            Description = Description,
            Maintainer = appUsers.Find(x => x.Email == user)
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
