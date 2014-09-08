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

        // TODO: make this required AFTER upgrading default initializers (and existing data?)
        public Project Project { get; set; }

        // TODO: migrate this to a User
        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        // TODO: migrate this to a user
        public string AssignedTo { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        // TODO: migrate this to a user
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

        public User DefaultUser { get; set; }
    }

    public class User
    {
        [Required]
        public int ID { get; set; }

        [StringLength(100, MinimumLength = 1)]
        public string FirstName { get; set; }

        [StringLength(100, MinimumLength = 2)]
        public string LastName { get; set; }

        // TODO: user level/role?
        // or handle that all through security? 
        // don;t know the security model well enough....

    }
}