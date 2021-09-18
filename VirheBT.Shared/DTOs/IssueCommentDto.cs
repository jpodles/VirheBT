using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirheBT.Shared.DTOs
{
    public class IssueCommentDto
    {
        public int CommentId { get; set; }

        public string Text { get; set; }
        public DateTime Created { get; set; }
        public ApplicationUserDto User { get; set; }
    }
}
