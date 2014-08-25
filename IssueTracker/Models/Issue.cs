using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IssueTracker.Models
{
    public class Issue
    {
        public int IssueId { get; set; }

        [Required(ErrorMessage="A Title is required.")]
        [StringLength(100, MinimumLength=5)]
        public string Title { get; set; }

        [Required(ErrorMessage="A Detailed Description is required.")]
        public string Description { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public string AssignedTo { get; set; }
        public DateTime? LastUpdatedDate { get; set; }

        public string ClosedBy { get; set; }
        public DateTime? ClosedDate { get; set; }

        public virtual ICollection<WorkNote> WorkNotes { get; set; }
    }
}