using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirheBT.State
{
    public interface IAppState
    {
        public int CurrentProjectId { get; set; }
        public int CurrentIssueId { get; set; }
    }
}
