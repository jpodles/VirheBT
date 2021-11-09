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
    private List<ApplicationUser> appUsers = new List<ApplicationUser>();
    private List<ApplicationUser> allowedUsers = new List<ApplicationUser>();
    private string selectedSearchValue { get; set; }

    private void MySearchHandler(string newValue) => selectedSearchValue = newValue;

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