using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
namespace freelance.Models
{
    [Table("Table")]
    public class Client
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "User Type")]
        public string UserType { get; set; }

        [Required]
        [Display(Name = "first Name")]
        public string firstName { get; set; }

        [Required]
        [Display(Name = "last Name")]
        public string lastName { get; set; }

        [Display(Name = "phone")]
        public string phone { get; set; }
    }


    public class Post
    {
        public string id = Guid.NewGuid().ToString();

        [Required]
        [Display (Name = "Job Type")]
        public string jobType { get; set; }

        [Required]
        [Display(Name = "Budget")]
        public double budget { get; set; }

        [Display(Name = "post Date")]
        public string postTime { get; set; }

        [Required]
        [Display(Name = "proposal Number")]
        public int proposalnum { get; set; }

        [Required]
        [Display(Name = "Job Description")]
        public string JobDescription { get; set; }

        public string ClientName { get; set; }
        public string ClientEmail { get; set; }
        public string state { get; set; }
    }

}
