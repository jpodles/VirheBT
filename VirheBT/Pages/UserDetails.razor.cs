namespace VirheBT.Pages;

public partial class UserDetails
{
    [Parameter]
    public string UserId { get; set; }

    [Inject]
    public IApplicationUserService ApplicationUserService { get; set; }

    private string UserName { get; set; }
    private string Email { get; set; }
    private string UserRole { get; set; }
    private string PhoneNumber { get; set; }

    private List<Project> Projects = new List<Project>();

    protected async Task GetUserData()
    {
        var user = await ApplicationUserService.GetApplicationUserAsync(UserId);
        UserName = user.FirstName + " " + user.LastName;
        Email = user.Email;
        UserRole = user.UserRole.ToString();
        PhoneNumber = user.PhoneNumber;
        Projects = user.Projects.ToList();
    }
}
