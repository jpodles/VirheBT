using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VirheBT.Infrastructure.Data.Models;

namespace VirheBT.Services.Interfaces
{
    public interface IIssueService
    {
        IEnumerable<Issue> GetIssues(int projecId);
        Issue GetIssue(int projectId, int issueId);

        void AddIssue(int projectId, Issue issue);
        void EditIssue(int projectId, int issueId, Issue issue);
        void DeleteIssue(int projectId, int issueId);


        IEnumerable<IssueComment> GetIssueComments(int projectId, int issueId);

        void AddComment(int projectId, int issueId, IssueComment comment);
        void EditComment(int projectId, int issueId, IssueComment comment);
        void DeleteComment(int projectId, int issueId, int commentId);
    }
}
