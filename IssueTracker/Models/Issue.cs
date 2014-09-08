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
        [Required]
        public int IssueId { get; set; }

        [Required(ErrorMessage="A Title is required.")]
        [StringLength(100, MinimumLength=5)]
        public string Title { get; set; }

        [Required(ErrorMessage="A Detailed Description is required.")]
        public string Description { get; set; }

        [Required]
        public int Project { get; set; }

        // TODO: this _should_ be required
        // but I don't yet understand how to assign properly
        //[Required]
        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string AssignedTo { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public string ClosedBy { get; set; }

        public DateTime? ClosedDate { get; set; }

        // TODO: worknotes needs to have an attachment option
        // also.... they should be sortable, right? to display in order
        public virtual ICollection<WorkNote> WorkNotes { get; set; }
    }

    public class Project
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "A Name is required.")]
        [StringLength(100, MinimumLength=3)]
        public string Name { get; set; }

        public string Description { get; set; }

        // TODO: how would this be locked down to a real user?
        public string  DefaultUser { get; set; }
    }
    
    public class User
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [StringLength(50, MinimumLength=4)]
        // this is not the same as the first/last name
        public string Name { get; set; }

        [StringLength(100, MinimumLength = 0)]
        public string FirstName { get; set; }

        [StringLength(100, MinimumLength = 0)]
        public string LastName { get; set; }

        // TODO: user level/role?
        // or handle that all through security? 
        // don;t know the security model well enough....

    }
}