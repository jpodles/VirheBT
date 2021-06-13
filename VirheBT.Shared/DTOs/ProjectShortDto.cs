using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VirheBT.Shared.Enums;


namespace VirheBT.Shared.DTOs
{
    public class ProjectShortDto
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public ProjectStatus Status { get; set; }
        public string Maintainer { get; set; }

    }
}
