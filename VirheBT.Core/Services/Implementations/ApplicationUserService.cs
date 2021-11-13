namespace VirheBT.Services.Implementations;

public class ApplicationUserService : IApplicationUserService
{
    private readonly IApplicationUserRepository _userRepository;
    private readonly IMapper _mapper;

    public ApplicationUserService(IApplicationUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public void DeactivateUserAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateUserAsync(ApplicationUserDto applicationUser, string userId)
    {
       // await _userRepository.UpdateUserAsync(applicationUser, userId);
    }

    public async Task<ApplicationUserDto> GetApplicationUserAsync(string userId)
    {
        var user = await _userRepository.GetApplicationUserAsync(userId);
        return _mapper.Map<ApplicationUserDto>(user);
    }

    public async Task<ApplicationUserDto> GetApplicationUserByEmailAsync(string userEmail)
    {
        var user = await _userRepository.GetApplicationUserByEmailAsync(userEmail);
        return _mapper.Map<ApplicationUserDto>(user);
    }

    public async Task<List<ApplicationUserDto>> GetApplicationUsersAsync()
    {
        var users = (await _userRepository.GetApplicationUsersAsync()).ToList();
        var usersDtos = new List<ApplicationUserDto>();
        foreach (var item in users)
            usersDtos.Add(_mapper.Map<ApplicationUserDto>(item));
        return usersDtos;
    }
}