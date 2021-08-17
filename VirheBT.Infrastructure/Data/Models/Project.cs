using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using VirheBT.Shared.Enums;

namespace VirheBT.Infrastructure.Data.Models
{
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectId { get; set; }

        public ApplicationUser Maintainer { get; set; }
        public string MaintainerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public ProjectStatus Status { get; set; }

        public ICollection<ApplicationUser> ApplicationUsers = new List<ApplicationUser>();

        public IEnumerable<Issue> Issues = new List<Issue>();
    }
}