using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodeHire.Models
{
    public class Resume
    {
        public int Id { get; set; }

        [Required]
        public ApplicationUser User { get; set; } = null!;

        public string Summary { get; set; }

        public List<JobHistory> WorkHistory { get; } = new();
    }
}