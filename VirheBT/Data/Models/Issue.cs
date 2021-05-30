using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VirheBT.Data.Enums;

namespace VirheBT.Data.Models
{
    public class Issue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public IssueStatus Status { get; set; }
        public IssuePriority Priority { get; set; }
        public IssueType Type { get; set; }

        [ForeignKey("CreatedById")]
        public ApplicationUser CreatedBy { get; set; }
        public int CreatedById { get; set; }

        [ForeignKey("AssignedToId")]
        public ApplicationUser AssignedTo { get; set; }
        public int AssignedToId { get; set; }

        [ForeignKey("ProjectId")]
        public Project Project { get; set; }
        public int ProjectId { get; set; }

        public ICollection<IssueComment> IssueComments { get; set; }
         = new List<IssueComment>();

        public ICollection<IssueHistory> IssueHistory { get; set; }
        = new List<IssueHistory>();
    }
}
