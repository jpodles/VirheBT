using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VirheBT.Shared.Enums;

namespace VirheBT.Infrastructure.Data.Models
{
    public class IssueHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HistoryId { get; set; }
        public ChangeType ChangeType { get; set; }

        public DateTime ChangeDate { get; set; }

        public int IssueId { get; set; }
        public Issue Issue { get; set; }


        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
    }
}
