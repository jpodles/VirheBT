namespace VirheBT.Services.Interfaces;

public interface IApplicationUserService
{
    Task<List<ApplicationUser>> GetApplicationUsersAsync();

    Task<ApplicationUser> GetApplicationUserAsync(string userId);

    Task<ApplicationUser> GetApplicationUserByEmailAsync(string userEmail);

    Task UpdateUserAsync(ApplicationUser applicationUser, string userId);

    void DeactivateUserAsync(string userId);
}
