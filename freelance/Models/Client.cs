using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace freelance.Models
{
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
        [Required]
        [Display(Name = "JobType")]
        public string jobType { get; set; }

        [Required]
        [Display(Name = "setBudget")]
        public double budget { get; set; }

        [Required]
        [Display(Name = "postDate")]
        public DateTime postTime { get; set; }

        [Required]
        [Display(Name = "proposalNumber")]
        public int proposalnum { get; set; }

        [Required]
        [Display(Name = "JobDescription")]
        public string JobDescription { get; set; }
    }
  
}
