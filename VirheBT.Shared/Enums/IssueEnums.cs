using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirheBT.Shared.Enums
{

    public enum IssuePriority
    {
        Low = 0,
        Normal = 1,
        High = 2
    }

    public enum IssueStatus

    {
        ToDo = 0,
        InProgress = 1,
        Done = 2
    }
    public enum IssueType
    {
        Bug = 0,
        Feature = 1
    }

    public enum ChangeType
    {
        Created = 0,
        Modified = 1,
        Closed = 2
    }



}
