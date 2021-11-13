namespace VirheBT.Pages;

public partial class CreateProject
{
    private string title;
    private string description;

    [Inject]
    private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

    [Inject]
    private IProjectService ProjectService { get; set; }

    [Inject]
    private IApplicationUserService ApplicationUserService { get; set; }

    private Blazorise.Modal createProjectModal;
    private List<ApplicationUserDto> appUsers = new List<ApplicationUserDto>();
    private List<ApplicationUserDto> allowedUsers = new List<ApplicationUserDto>();
    private string selectedSearchValue { get; set; }

    private void MySearchHandler(string newValue) => selectedSearchValue = newValue;

    public async Task OnCreateAsync()
    {
        var authState = await AuthenticationStateProvider
            .GetAuthenticationStateAsync();
        var user = authState.User.Identity.Name;

        var projectToCreate = new CreateProjectDto
        {
            Title = title,
            Description = description,
            MaintainerEmail = user
        };
        await ProjectService.CreateProjectAsync(projectToCreate);
        title = "";
        description = "";

        NavigationManager.NavigateTo("/projects", true);
    }

    public void ProjectTitleValidator(ValidatorEventArgs e)
    {
        var title = Convert.ToString(e.Value);
        e.Status = string.IsNullOrEmpty(title) ? ValidationStatus.None : ValidationStatus.Success;
    }

    public bool CanAdd() => !string.IsNullOrEmpty(title);

    public async Task ShowModal()
    {
        appUsers = await ApplicationUserService.GetApplicationUsersAsync();
        allowedUsers = appUsers.FindAll(x => x.UserRole is UserRole.ProjectManager or UserRole.Admin);
        createProjectModal.Show();
    }

    private void HideModal() => createProjectModal.Hide();
}