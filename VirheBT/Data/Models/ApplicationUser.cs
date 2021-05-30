using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

using VirheBT.Data.Enums;

namespace VirheBT.Data.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public UserStatus UserStatus { get; set; }
        public List<Project> Projects { get; set; }
    }
}
