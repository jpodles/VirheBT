using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirheBT.Data.Models
{
    public class IssueComment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }

        [ForeignKey("IssueId")]
        public Issue Issue { get; set; }
        public int IssueId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public int UserId { get; set; }
    }
}
