using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using VirheBT.Shared.Enums;

namespace VirheBT.Infrastructure.Data.Models
{
    public class Issue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IssueId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public IssueStatus Status { get; set; }
        public IssuePriority Priority { get; set; }
        public IssueType Type { get; set; }

        public ApplicationUser CreatedBy { get; set; }
        public string CreatedById { get; set; }

        public ApplicationUser AssignedTo { get; set; }
        public string AssignedToId { get; set; }

        public ApplicationUser ModifiedBy { get; set; }
        public string ModifiedById { get; set; }

        public Project Project { get; set; }
        public int ProjectId { get; set; }

        public ICollection<IssueComment> IssueComments { get; set; }
         = new List<IssueComment>();

        public ICollection<IssueHistory> IssueHistory { get; set; }
        = new List<IssueHistory>();
    }
}