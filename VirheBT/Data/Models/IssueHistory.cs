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
    public class IssueHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public ChangeType ChangeType { get; set; }

        public DateTime ChangeDate { get; set; }

        [ForeignKey("IssueId")]
        public Issue Issue { get; set; }
        public int IssueId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public int UserId { get; set; }
    }
}
