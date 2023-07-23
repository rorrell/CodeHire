using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodeHire.Dtos
{
    public class ResumeWithoutUserDto
    {
        public int Id { get; set; }

        [Required]
        public string Summary { get; set; }

        public List<JobHistoryDto> WorkHistory { get; } = new();

        public List<SkillDto> Skills { get; } = new();
    }
}