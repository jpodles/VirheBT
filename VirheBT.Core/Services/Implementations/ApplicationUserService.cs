namespace VirheBT.Services.Implementations;

public class ApplicationUserService : IApplicationUserService
{
    private readonly IApplicationUserRepository _userRepository;

    public ApplicationUserService(IApplicationUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void DeactivateUserAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateUserAsync(ApplicationUser applicationUser, string userId)
    {
        await _userRepository.UpdateUserAsync(applicationUser, userId);
    }

    public async Task<ApplicationUser> GetApplicationUserAsync(string userId)
    {
        return await _userRepository.GetApplicationUserAsync(userId);
    }

    public async Task<ApplicationUser> GetApplicationUserByEmailAsync(string userEmail)
    {
        return await _userRepository.GetApplicationUserByEmailAsync(userEmail);
    }

    public async Task<List<ApplicationUser>> GetApplicationUsersAsync()
    {
        var users = await _userRepository.GetApplicationUsersAsync();
        return users.ToList();
    }
}