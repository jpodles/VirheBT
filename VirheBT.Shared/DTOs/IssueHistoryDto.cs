using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VirheBT.Shared.Enums;

namespace VirheBT.Shared.DTOs
{
    public class IssueHistoryDto
    {
        public ChangeType ChangeType { get; set; }
        public DateTime ChangeDate { get; set; }
        public ApplicationUserDto User { get; set; }

    }
}
