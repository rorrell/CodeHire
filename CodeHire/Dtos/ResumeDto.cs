using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodeHire.Dtos
{
    public class ResumeDto
    {
        public int Id { get; set; }

        [Required]
        public UserDto User { get; set; } = null!;

        [Required]
        public string Summary { get; set; }

        public List<JobHistoryDto> WorkHistory { get; } = new();
    }
}