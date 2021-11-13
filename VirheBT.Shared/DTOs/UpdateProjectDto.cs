using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VirheBT.Shared.Enums;

namespace VirheBT.Shared.DTOs
{
    public class UpdateProjectDto
    {
        public ApplicationUserDto Maintainer { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public ProjectStatus Status { get; set; }
    }
}
