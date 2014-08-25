using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IssueTracker.Models
{
    public class WorkNote
    {
        public int WorkNoteId { get; set; }

        [Required(ErrorMessage = "Details are required.")]
        public string Detail { get; set; }
        public DateTime LoggedDate { get; set; }

        public int IssueId { get; set; }
        [Display(Name="Note Author")]
        public string EnteredBy { get; set; }

        public virtual Issue AttachedIssue { get; set; }
    }
}