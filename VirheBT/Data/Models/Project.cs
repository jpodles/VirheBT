using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VirheBT.Data.Enums;

namespace VirheBT.Data.Models
{
    public class Project
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public ProjectStatus Status { get; set; }
        public ApplicationUser Maintainer { get; set; }
        public int MaintainerId { get; set; }

        public ICollection<ApplicationUser> Team = new List<ApplicationUser>();
    }
}
