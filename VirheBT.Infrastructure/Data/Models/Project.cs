using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VirheBT.Shared.Enums;

namespace VirheBT.Infrastructure.Data.Models
{
    public class Project
    {
        public int ProjectId { get; set; }

        public ApplicationUser Maintainer { get; set; }
        public string MaintainerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public ProjectStatus Status { get; set; }

        public ICollection<ApplicationUser> ApplicationUsers = new List<ApplicationUser>();

        public ICollection<Issue> Issues = new List<Issue>();
    }
}
