namespace VirheBT.Services.Interfaces;

public interface IApplicationUserService
{
    Task<List<ApplicationUserDto>> GetApplicationUsersAsync();

    Task<ApplicationUserDto> GetApplicationUserAsync(string userId);

    Task<ApplicationUserDto> GetApplicationUserByEmailAsync(string userEmail);

    Task UpdateUserAsync(ApplicationUserDto applicationUser, string userId);

    void DeactivateUserAsync(string userId);
}
