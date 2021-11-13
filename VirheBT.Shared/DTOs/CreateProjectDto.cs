using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirheBT.Shared.DTOs
{
    public record class CreateProjectDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string MaintainerEmail { get; set; }
    }
}
