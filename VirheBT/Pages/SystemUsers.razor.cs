namespace VirheBT.Pages;

public class UserDto
{
    public int ID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public UserRole UserRole { get; set; }
    public UserStatus UserStatus { get; set; }
}

public partial class SystemUsers
{
    private bool editable = true;
    private bool sortable = true;
    private bool filterable = true;
    private bool showPager = true;
    private DataGridEditMode editMode = DataGridEditMode.Popup;
    private DataGridSortMode sortMode = DataGridSortMode.Single;
    private DataGridSelectionMode selectionMode = DataGridSelectionMode.Single;
    private DataGridCommandMode commandsMode = DataGridCommandMode.Default;
    public DataGrid<ApplicationUserDto> dataGrid;
    public int currentPage { get; set; } = 1;
    private List<ApplicationUserDto> data = new List<ApplicationUserDto>();

    [Inject]
    private IApplicationUserService ApplicationUserService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        data = await ApplicationUserService.GetApplicationUsersAsync();
    }

    private async Task OnRowUpdated(SavedRowItem<ApplicationUserDto, Dictionary<string, object>> e)
    {
        await ApplicationUserService.UpdateUserAsync(e.Item, e.Item.Id);
        data = await ApplicationUserService.GetApplicationUsersAsync();
        StateHasChanged();
    }
}
