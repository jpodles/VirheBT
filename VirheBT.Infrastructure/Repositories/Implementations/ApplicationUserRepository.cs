namespace VirheBT.Infrastructure.Repositories.Implementations;

public class ApplicationUserRepository : IApplicationUserRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> userManager;

    public ApplicationUserRepository(ApplicationDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _mapper = mapper;
        this.userManager = userManager;
    }

    public async void DeactivateUserAsync(string userId)
    {
        var userEntity = await GetApplicationUserAsync(userId);
        userEntity.UserStatus = UserStatus.Inactive;
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(ApplicationUser applicationUser, string userId)
    {
        var userEntity = await GetApplicationUserAsync(userId);

        var roles = await userManager.GetRolesAsync(userEntity);
        await userManager.RemoveFromRolesAsync(userEntity, roles.ToArray());
        await userManager.AddToRoleAsync(userEntity, applicationUser.UserRole.ToString());

        userEntity.FirstName = applicationUser.FirstName;
        userEntity.LastName = applicationUser.LastName;
        userEntity.UserStatus = applicationUser.UserStatus;
        userEntity.UserRole = applicationUser.UserRole;

        await _context.SaveChangesAsync();
    }

    public async Task<ApplicationUser> GetApplicationUserAsync(string userId)
    {
        return await _context.ApplicationUsers.Include(x => x.Projects)
            .Where(u => u.Id == userId).FirstOrDefaultAsync();
    }

    public async Task<ApplicationUser> GetApplicationUserByEmailAsync(string userEmail)
    {
        return await _context.ApplicationUsers
            .Where(u => u.Email == userEmail).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<ApplicationUser>> GetApplicationUsersAsync()
    {
        return await _context.ApplicationUsers.ToListAsync();
    }
}