using VirheBT.Shared.Enums;

namespace VirheBT.Shared.DTOs
{
    public class ApplicationUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public UserRole UserRole { get; set; }
    }
}