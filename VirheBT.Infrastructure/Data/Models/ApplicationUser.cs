using Microsoft.AspNetCore.Identity;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using VirheBT.Shared.Enums;

namespace VirheBT.Infrastructure.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public UserStatus UserStatus { get; set; }
        public ICollection<Project> Projects { get; set; } = new List<Project>();

        public UserRole UserRole { get; set; }

        public ICollection<Project> ProjectsMaintained { get; set; }
    }
}