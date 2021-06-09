using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;


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
    }
}
