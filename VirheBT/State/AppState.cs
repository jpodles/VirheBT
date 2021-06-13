using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirheBT.State
{
    public class AppState : IAppState
    {
        public int CurrentProjectId { get; set; }
        public int CurrentIssueId { get; set; }
    }
}
