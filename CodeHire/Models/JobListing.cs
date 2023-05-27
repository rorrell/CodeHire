using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodeHire.Models
{
    public class JobListing
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [StringLength(255)]
        public string Company { get; set; }

        [Required]
        [StringLength(255)]
        public string Location { get; set; }

        public string Description { get; set; }

        [Display(Name = "Expiration Date")]
        public DateTime? ExpirationDate { get; set; }

        public string Wage { get; set; }

        public List<Language> Languages { get; } = new();

        public List<ApplicationUser> ApplicationUsers { get; } = new();
    }
}