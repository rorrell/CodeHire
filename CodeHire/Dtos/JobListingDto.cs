﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CodeHire.Models;

namespace CodeHire.Dtos
{
    public class JobListingDto
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

        public IEnumerable<LanguageDto> Languages { get; set; }
    }
}