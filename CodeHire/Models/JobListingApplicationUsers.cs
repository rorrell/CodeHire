using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeHire.Models
{
    public class JobListingApplicationUser
    {
        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUser_Id { get; set; }

        public JobListing JobListing { get; set; }
        public int JobListing_Id { get; set; }

        public JobListingType Type { get; set; }
    }

    public enum JobListingType
    {
        Applied,
        Saved
    }
}