using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodeHire.Dtos
{
    public class JobHistoryDto
    {
        public int Id { get; set; }

        [Required]
        public string JobTitle { get; set; }

        [Required]
        public string Company { get; set; }

        public string Location { get; set; }

        public DateTime StartDate { get; set; }

        public string StartDateString => StartDate.ToShortDateString();

        public DateTime EndDate { get; set; }

        public string EndDateString => EndDate.ToShortDateString();

        public string Description { get; set; }
    }
}